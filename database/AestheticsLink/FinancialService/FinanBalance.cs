using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace FinancialService
{
    [DisallowConcurrentExecution]
    public class FinanBalance : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await FinancialStatic();
        }
        private async Task FinancialStatic()
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();

                var dateNow = DateTime.Now;
                //对每个医院分开统计
                var hospitalList = await DbContext.db.Ado.SqlQueryAsync<HOSPITAL>("SELECT * from HOSPITAL");
                foreach (var hospital in hospitalList)
                {
                    int incomeTotal = 0;
                    //统计总订单收入
                    var income = await DbContext.db.Ado.SqlQueryAsync<int>("SELECT PAID_AMOUNT from BILL WHERE HOS_ID = :hosID AND TO_CHAR(FOUND_DATE, 'YYYY-MM') = TO_CHAR(:date, 'YYYY-MM')",
                        new
                        {
                            hosID = hospital.HOS_ID,
                            date = dateNow.Date,
                        });
                    foreach (var i in income)
                        incomeTotal += i;

                    int payoutTotal = 0;
                    //统计员工总工资
                    var salary = await DbContext.db.Ado.SqlQueryAsync<int>("SELECT TAKEHOMEPAY from SERVER WHERE HOS_ID = :hosID",
                        new
                        {
                            hosID = hospital.HOS_ID,
                        });
                    foreach (var i in salary)
                        payoutTotal += i;
                    //统计员工总工资
                    var basicsalary = DbContext.db.Ado.SqlQuery<int>("SELECT BASICSALARY from SERVER WHERE HOS_ID = :hosID",
                        new
                        {
                            hosID = hospital.HOS_ID,
                        });
                    foreach (var i in basicsalary)
                        payoutTotal += i;
                    //更改医院收支
                    await DbContext.db.Ado.ExecuteCommandAsync(
                        "UPDATE FINANCIAL " +
                        "SET INCOME = INCOME + :income, PAYOUT = PAYOUT + :payout  " +
                        "WHERE HOS_ID = :hosID AND TO_CHAR(FINANCE_MONTH, 'YYYY-MM') = TO_CHAR(:month, 'YYYY-MM')",
                        new
                        {
                            income = incomeTotal,
                            payout = payoutTotal,
                            hosID = hospital.HOS_ID,
                            month = dateNow.Date,
                        });
                }
                //事物提交
                DbContext.db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                throw (ex);
            }
        }

    }
}
