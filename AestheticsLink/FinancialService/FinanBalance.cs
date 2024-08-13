using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;

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
                var hospitalList = await DbContext.db.Ado.SqlQueryAsync<string>("SELECT HOS_ID, NAME from HOSPITAL");
                foreach (var hospital in hospitalList)
                {
                    int incomeTotal = 0;
                    //统计总订单收入
                    var income = await DbContext.db.Ado.SqlQueryAsync<int>("SELECT PAID_AMOUNT from BILL WHERE HOS_ID = :hosID AND FOUND_DATE = :date",
                        new
                        {
                            hosID = hospital,
                            date = dateNow.Date,
                        });
                    foreach (var i in income)
                        incomeTotal += i;

                    int payoutTotal = 0;
                    //统计员工总工资
                    var salary = await DbContext.db.Ado.SqlQueryAsync<int>("SELECT TAKEHOMEPAY from SERVER WHERE HOS_ID = :hosID",
                        new
                        {
                            hosID = hospital,
                        });
                    foreach (var i in salary)
                        payoutTotal += i;
                    //更改医院收支
                    await DbContext.db.Ado.ExecuteCommandAsync(
                        "UPDATE FINANCIAL " +
                        "SET INCOME = :income, PAYOUT = PAYOUT + :payout  " +
                        "WHERE HOS_ID = :hosID AND FINANCE_MONTH = :month",
                        new
                        {
                            income = incomeTotal,
                            payout = payoutTotal,
                            hosID = hospital,
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
