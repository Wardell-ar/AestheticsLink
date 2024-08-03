using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;

namespace FinancialService
{
    [DisallowConcurrentExecution]
    public class FinancialFound : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await FinancialInsert();
        }
        private async Task FinancialInsert()
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();

                var hospitalList = DbContext.db.Ado.SqlQuery<string>("SELECT HOS_ID, NAME from HOSPITAL");
                foreach (var hospital in hospitalList)
                {
                    var day = DateTime.Now;
                    int payoutTotal = 0;
                    //统计员工总工资
                    var salary = DbContext.db.Ado.SqlQuery<int>("SELECT BASICSALARY from SERVER WHERE HOS_ID = :hosID",
                        new
                        {
                            hosID = hospital,
                        });
                    foreach (var i in salary)
                        payoutTotal += i;

                    await DbContext.db.Ado.ExecuteCommandAsync(
                        "INSERT INTO FINANCIAL " +
                        "VALUES (:hosID, :month, :income, :payout)",
                        new
                        {
                            hosID = hospital,
                            month = day.Date,
                            income = 0,
                            payout = payoutTotal,
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
