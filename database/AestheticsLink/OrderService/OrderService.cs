using Oracle.ManagedDataAccess.Client;
using OrderService.Dto;
using WebCommon.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebModel.Entity;
using Microsoft.Win32;
using OperateService.Dto;

namespace OrderService
{
    public class OrdersService : IOrderService
    {
        public bool CheckProject(PlaceOrderDto order)
        {
            //检查项目是否正确
            List<string> projects = order.items;
            foreach (string project in projects)
            {
                var result = DbContext.db.Ado.SqlQuery<ProjectDto>(
                        "SELECT PROJ_ID FROM PROJECT WHERE NAME = :name",
                        new { name = project }
                    );
                if (result.Count == 0)
                {
                    return false;
                }
            }
            //检查客户是否存在,余额是否足够
            int paid = 0;
            foreach (string project in order.items)
            {
                var money = DbContext.db.Ado.SqlQuerySingle<int>(
                        "SELECT PRICE FROM PROJECT WHERE NAME = :name",
                        new { name = project }
                    );
                //计算订单金额
                paid += money;
            }
            var viplevel = DbContext.db.Ado.SqlQuerySingle<MEMBER>(
                        "SELECT * FROM CUSTOMER NATURAL JOIN MEMBER WHERE CUS_ID = :id ",
                        new
                        {
                            id = order.clientid,
                        });
            decimal pay = paid * viplevel.DISCOUNT;
            decimal price;
            if (order.couponid != null)
            {
                price = DbContext.db.Ado.SqlQuerySingle<decimal>(
                    "SELECT PRICE FROM COUPON WHERE COU_ID = :id ",
                    new
                    {
                        id = order.couponid,
                    });
                if (pay > price)
                    pay -= price;
                else
                    pay = 0;
            }
            var customer = DbContext.db.Ado.SqlQuery<CUSTOMER>(
                        "SELECT CUS_ID FROM CUSTOMER WHERE CUS_ID = :cus_ID AND BALANCE >= : paidAmount",
                        new
                        {
                            cus_ID = order.clientid,
                            paidAmount = pay
                        }
                    );
            if (customer.Count == 0)
            {
                return false;
            }
            //检查医院是否存在
            if (!DbContext.db.Queryable<HOSPITAL>().Any(c => c.NAME.Equals(order.hospital)))
                return false;
            //检查优惠券是否正确
            if (order.couponid != "")
            {
                var check = DbContext.db.Ado.SqlQuery<PlaceOrderDto>(
                    "SELECT CUS_ID " +
                    "FROM CUS_COU " +
                    "WHERE CUS_ID = :cusId AND COU_ID = :couID",
                    new
                    {
                        cusId = order.clientid,
                        couID = order.couponid,
                    });
                if (check.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public List<HospitalChoiceDto> GetHospital()
        {
            List<HospitalChoiceDto> result = DbContext.db.Ado.SqlQuery<HospitalChoiceDto>("SELECT HOS_ID, NAME from HOSPITAL");
            return result;
        }

        public BillDto AddBill(PlaceOrderDto order)
        {
            BILL bill = TransBillDto(order);
            try
            {
                DbContext.db.Insertable(bill).ExecuteCommand();
                //为每个项目添加手术安排
                OPERATE operate = new OPERATE();
                List<string> projects = order.items;
                var timeNow = DateTime.Now;
                //医生和手术时间、手术室自动分配
                //找到可用的时间和手术室,从当前时间往后找找到第一个可用时间
                int i = 0;
                List<OperateTimeDto> thisSecheduledTimes = new List<OperateTimeDto>();
                foreach (string project in projects)
                {
                    // 先找到项目ID
                    var projId = DbContext.db.Ado.SqlQuerySingle<string>(
                        "SELECT PROJ_ID FROM PROJECT WHERE NAME = :name",
                        new
                        {
                            name = project,
                        });


                    // 查找该手术室的可用时间
                    var availableTimes = DbContext.db.Ado.SqlQuery<OperateTimeDto>(
                        "SELECT * FROM OPERATE_TIME WHERE STATUS = :status AND START_TIME > :time AND DAY >= :day ORDER BY START_TIME ASC",
                        new
                        {
                            status = "0",
                            time = timeNow,
                            day = timeNow.Date,
                        });
                    // 遍历该手术室的所有可用时间段
                    foreach (var availableTime in availableTimes)
                    {
                        // 查找客户已经安排的手术时间段
                        var scheduledTimes = DbContext.db.Ado.SqlQuery<OperateTimeDto>(
                            "SELECT START_TIME, END_TIME FROM OPERATE_TIME NATURAL JOIN OPERATE NATURAL JOIN BILL WHERE CUS_ID = :customerId AND DAY >= :day",
                            new
                            {
                                customerId = order.clientid,
                                day = timeNow.Date,
                            });
                        // 检查当前可用时间段是否与客户的已安排手术时间段冲突
                        bool hasConflict = scheduledTimes.Any(st =>
                            availableTime.START_TIME < st.END_TIME && availableTime.END_TIME > st.START_TIME);
                        bool hasConflicts = false;
                        if (thisSecheduledTimes.Count != 0)
                        {
                            hasConflicts = thisSecheduledTimes.Any(st =>
                                availableTime.START_TIME < st.END_TIME && availableTime.END_TIME > st.START_TIME);
                        }
                        // 如果没有冲突，则为该时间段安排手术
                        if (!hasConflict && !hasConflicts)
                        {
                            // 找到该医院中在此时间可用的医生
                            string hosID = DbContext.db.Ado.SqlQuerySingle<string>(
                                "SELECT HOS_ID FROM HOSPITAL WHERE NAME = :name",
                                new { name = order.hospital });

                            // 查询没有在该时间段被安排手术的医生
                            var availableDoctors = DbContext.db.Ado.SqlQuery<SERVER>(
                                "SELECT SER_ID " +
                                "FROM SERVER " +
                                "WHERE SER_ID NOT IN (" +
                                    "SELECT SER_ID " +
                                    "FROM OPERATE " +
                                    "NATURAL JOIN OPERATE_TIME " +
                                    "WHERE STATUS = :status AND START_TIME = :time AND DAY = :day ) " +
                                "AND HOS_ID = :hos AND POSITION = :pos ",
                                new
                                {
                                    status = "1",  // 已占用状态
                                    time = availableTime.START_TIME,
                                    day = availableTime.DAY,
                                    hos = hosID,
                                    pos = "医生",
                                });

                            // 检查是否找到可用医生
                            if (availableDoctors != null && availableDoctors.Count > 0)
                            {
                                // 选择第一个医生
                                //var selectedDoctor = availableDoctors.First();
                                var random = new Random();
                                var selectedDoctor = availableDoctors[random.Next(availableDoctors.Count())];

                                // 为手术分配找到的项目、医生和时间
                                operate.SER_ID = selectedDoctor.SER_ID;
                                operate.PROJ_ID = projId;
                                operate.BILL_ID = bill.BILL_ID;
                                operate.FOUND_DATE = bill.FOUND_DATE;
                                operate.EXE_STATE = "0";  // 未执行状态
                                operate.OP_TIME_ID = availableTime.OP_TIME_ID;

                                // 更新该时间段为已占用
                                DbContext.db.Ado.ExecuteCommand(
                                    "UPDATE OPERATE_TIME SET STATUS = :status WHERE OP_TIME_ID = :timeID",
                                    new
                                    {
                                        status = "1",  // 设置为已占用
                                        timeID = availableTime.OP_TIME_ID,
                                    });
                                thisSecheduledTimes.Add(availableTime);
                                DbContext.db.Insertable(operate).ExecuteCommand();
                                // 成功安排后退出循环
                                break;
                            }
                        }
                    }

                }
                if (operate == null)
                    return null;
                

                return new BillDto
                {
                    BILL_ID = bill.BILL_ID,
                    CUS_ID = bill.CUS_ID,
                    COU_ID = bill.COU_ID,
                    FOUND_DATE = bill.FOUND_DATE,
                    PAID_AMOUNT = bill.PAID_AMOUNT,
                    HOS_ID = bill.HOS_ID,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("订单创建失败");
            }

        }
        private BILL TransBillDto(PlaceOrderDto order)
        {
            var bill = new BILL();
            var foundDate = DateTime.Now;
            // 获取数据库中已存在的最大 CUS_ID
            string lastCustomerId = GetMaxBillId();
            // 生成新的 CUS_ID
            int newId;
            if (lastCustomerId == "")
            {
                newId = 1;
            }
            else
            {
                newId = int.Parse(lastCustomerId) + 1;
            }
            bill.BILL_ID = newId.ToString();// 自己设置
            bill.CUS_ID = order.clientid;
            bill.COU_ID = order.couponid;
            bill.FOUND_DATE = foundDate;
            int paid = 0;
            foreach (string project in order.items)
            {
                var money = DbContext.db.Ado.SqlQuerySingle<int>(
                        "SELECT PRICE FROM PROJECT WHERE NAME = :name",
                        new { name = project }
                    );
                //计算订单金额
                paid += money;
            }
            bill.PAID_AMOUNT = paid;
            var hosId = DbContext.db.Ado.SqlQuerySingle<string>(
                       "SELECT HOS_ID FROM HOSPITAL WHERE NAME = :name",
                       new { name = order.hospital }
                   );
            bill.HOS_ID = hosId;

            return bill;

        }
        private string GetMaxBillId()
        {
            string sql = "SELECT MAX(TO_NUMBER(BILL_ID)) FROM BILL"; 

            // 执行 SQL 查询
            var result = DbContext.db.Ado.GetString(sql);

            return result;
        }

        public bool CheckCustomer(Cus_CouReceptionDto cus_CouReception)
        {
            var result = DbContext.db.Ado.SqlQuery<CUSTOMER>(
                        "SELECT CUS_ID FROM CUSTOMER WHERE CUS_ID = :cusId",
                        new { cusId = cus_CouReception.id }
                    );
            if (result.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
