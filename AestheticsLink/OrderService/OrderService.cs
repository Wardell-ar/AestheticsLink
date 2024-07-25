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

namespace OrderService
{
    public class OrdersService : IOrderService
    {
        public bool CheckProject(PlaceOrderDto order)
        {
            //检查项目是否正确
            List<ProjectDto> projects = order.PROJECT;
            foreach (ProjectDto project in projects) 
            {
                var result = DbContext.db.Ado.SqlQuery<ProjectDto>(
                        "SELECT PROJ_ID FROM PROJECT WHERE PROJ_ID = :projectId",
                        new { projectId = project.PROJ_ID }
                    );
                if (result.Count == 0)
                {
                    return false;
                }
            }
            //检查客户是否存在,余额是否足够
            var customer = DbContext.db.Ado.SqlQuery<CUSTOMER>(
                        "SELECT CUS_ID FROM CUSTOMER WHERE CUS_ID = :cus_ID AND BALANCE >= : paidAmount",
                        new 
                        { 
                            cus_ID = order.CUS_ID, 
                            paidAmount = order.PAID_AMOUNT
                        }
                    );
            if( customer.Count == 0 )
            {
                return false;
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
                List<ProjectDto> projects = order.PROJECT;
                foreach (ProjectDto project in projects)
                {
                    operate.PROJ_ID = project.PROJ_ID;
                    operate.BILL_ID = bill.BILL_ID;
                    operate.FOUND_DATE = bill.FOUND_DATE;
                    operate.EXE_STATE = "0";
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
                    COMPLE_STATE = bill.COMPLE_STATE,

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
            bill.CUS_ID = order.CUS_ID;
            bill.COU_ID = null;
            bill.FOUND_DATE = foundDate;
            bill.PAID_AMOUNT = 0;
            bill.HOS_ID = null;
            bill.COMPLE_STATE = "0";// 0为还未完全创建好，部分值仍待填写

            return bill;

        }
        private string GetMaxBillId()
        {
            string sql = "SELECT MAX(BILL_ID) FROM BILL"; // 替换YourTableName为实际的表名

            // 执行 SQL 查询
            var result = DbContext.db.Ado.GetString(sql);

            return result;
        }

        bool IOrderService.CheckInfo(RestChoiceDto rest)
        {
            //只有STATE为0才可以修改
            var state = DbContext.db.Ado.SqlQuery<RestChoiceDto>("SELECT BILL_ID FROM BILL WHERE COMPLE_STATE = 0");
            if (state.Count == 0)
            {
                return false;
            }
            //检查CUS_ID和BILL_ID是否存在且对应
            var result = DbContext.db.Ado.SqlQuery<RestChoiceDto>(
                "SELECT CUSTOMER.CUS_ID, BILL.BILL_ID " +
                "FROM CUSTOMER " +
                "JOIN BILL ON CUSTOMER.CUS_ID = BILL.CUS_ID " +
                "WHERE CUSTOMER.CUS_ID = :cusId AND BILL.BILL_ID = :billID",
                new
                {
                    cusId = rest.CUS_ID,
                    billID = rest.BILL_ID,
                });
            if (result.Count == 0)
            {
                return false;
            }
            //检查医院是否存在
            if (!DbContext.db.Queryable<HOSPITAL>().Any(c => c.HOS_ID.Equals(rest.HOS_ID)))
                return false;
            //检查优惠券是否正确
            var check = DbContext.db.Ado.SqlQuery<RestChoiceDto>(
                "SELECT CUS_ID " +
                "FROM CUS_COU " +
                "WHERE CUS_ID = :cusId AND COU_ID = :couID",
                new
                {
                    cusId = rest.CUS_ID,
                    couID = rest.COU_ID,
                });
            if (check.Count == 0)
            {
                return false;
            }
            //填写BILL的COU_ID和HOS_ID
            DbContext.db.Ado.ExecuteCommand(
                "UPDATE BILL SET HOS_ID =:hosID, COU_ID =:couID, PAID_AMOUNT =:paidAmount WHERE BILL_ID =:billID",
                new
                {
                    hosID = rest.HOS_ID,
                    couID = rest.COU_ID,
                    paidAmount = rest.PAID_AMOUNT,
                    billID = rest.BILL_ID,
                });
            //修改订单状态
            DbContext.db.Ado.ExecuteCommand(
                "UPDATE BILL SET COMPLE_STATE =:state WHERE BILL_ID =:billID",
                new
                {
                    state = "1",
                    billID = rest.BILL_ID,
                });

            return true;
        }
    }
}
