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
            List<ProjectDto> projects = order.items;
            foreach (ProjectDto project in projects) 
            {
                var result = DbContext.db.Ado.SqlQuery<ProjectDto>(
                        "SELECT PROJ_ID FROM PROJECT WHERE NAME = :name",
                        new { name = project.NAME }
                    );
                if (result.Count == 0)
                {
                    return false;
                }
            }
            //检查客户是否存在,余额是否足够
            int paid = 0;
            foreach (ProjectDto project in order.items)
            {
                var money = DbContext.db.Ado.SqlQuerySingle<int>(
                        "SELECT PRICE FROM PROJECT WHERE NAME = :name",
                        new { name = project.NAME }
                    );
                //计算订单金额
                paid += money;
            }
            var customer = DbContext.db.Ado.SqlQuery<CUSTOMER>(
                        "SELECT CUS_ID FROM CUSTOMER WHERE CUS_ID = :cus_ID AND BALANCE >= : paidAmount",
                        new 
                        { 
                            cus_ID = order.clientid, 
                            paidAmount = paid
                        }
                    );
            if( customer.Count == 0 )
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
                List<ProjectDto> projects = order.items;
                var timeNow = DateTime.Now;
                //医生和手术时间、手术室自动分配
                //找到可用的时间和手术室,从当前时间往后找找到第一个可用时间
                var availableTimes = DbContext.db.Ado.SqlQuery<OperateTimeDto>(
                    "SELECT OP_TIME_ID FROM OPERATE_TIME WHERE STATUS = :status AND START_TIME > :time AND DAY >= :day ",
                    new
                    {
                        status = "0",
                        time = timeNow,
                        day = timeNow.Date,
                    });
                int i = 0;
                foreach (ProjectDto project in projects)
                {
                    var id = DbContext.db.Ado.SqlQuerySingle<string>(
                    "SELECT PROJ_ID " +
                    "FROM PROJECT " +
                    "WHERE NAME = :name",
                    new
                    {
                        name = project.NAME,
                    });
                    operate.PROJ_ID = id;
                    operate.BILL_ID = bill.BILL_ID;
                    operate.FOUND_DATE = bill.FOUND_DATE;
                    operate.EXE_STATE = "0";
                    operate.OP_TIME_ID = availableTimes[i].OP_TIME_ID;
                    DbContext.db.Ado.ExecuteCommand(
                    "UPDATE OPERATE_TIME SET STATUS = :status WHERE OP_TIME_ID = :timeID",
                    new
                    {
                        status = "1",
                        timeID = availableTimes[i].OP_TIME_ID,
                    });
                    //找到在可用时间可用的医生
                    string hosID = DbContext.db.Ado.SqlQuerySingle<string>("SELECT HOS_ID FROM HOSPITAL WHERE NAME = :name", new { name = order.hospital });
                    var availableDoctors = DbContext.db.Ado.SqlQuery<SERVER>(
                        "SELECT SER_ID " +
                          "FROM SERVER " +
                          "WHERE SER_ID NOT IN (" +
                              "SELECT SER_ID " +
                              "FROM OPERATE " +
                              "NATURAL JOIN OPERATE_TIME " +
                              "WHERE STATUS = :status AND START_TIME = :time AND DAY = :day ) " +
                              "AND HOS_ID = :hos",
                        new
                        {
                            status = "1",
                            time = availableTimes[i].START_TIME,
                            day = availableTimes[i].DAY,
                            hos = hosID,
                        });
                    operate.SER_ID = availableDoctors[i].SER_ID;
                    i++;
                    DbContext.db.Insertable(operate).ExecuteCommand();
                }

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
            catch(Exception ex) 
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
            foreach (ProjectDto project in order.items)
            {
                var money = DbContext.db.Ado.SqlQuerySingle<int>(
                        "SELECT PRICE FROM PROJECT WHERE NAME = :name",
                        new { name = project.NAME }
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
            string sql = "SELECT MAX(TO_NUMBER(BILL_ID)) FROM BILL"; // 替换YourTableName为实际的表名

            // 执行 SQL 查询
            var result = DbContext.db.Ado.GetString(sql);

            return result;
        }

    }
}
