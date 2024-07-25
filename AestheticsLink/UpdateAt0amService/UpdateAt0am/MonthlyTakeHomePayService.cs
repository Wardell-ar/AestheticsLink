using Quartz;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;
using WebCommon.Database;

namespace UpdateAt0amService.UpdateAt0am
{
    public class MonthlyTakeHomePayService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await MonthlyTakeHomePay();
        }

        public async Task MonthlyTakeHomePay()
        {
            // 更新每个员工的TAKEHOMEPAY为BASICSALARY
            await DbContext.db.Updateable<SERVER>()
                .SetColumns(server => new SERVER { TAKEHOMEPAY = server.BASICSALARY })
                .Where(server => true) // 确保有Where条件
                .ExecuteCommandAsync();
        }
    }
}
