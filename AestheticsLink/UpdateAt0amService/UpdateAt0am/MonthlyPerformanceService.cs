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
    public class MonthlyPerformanceService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await MonthlyPerformance();
        }

        public async Task MonthlyPerformance()
        {
            // 获取上个月的开始和结束日期
            DateTime now = DateTime.Now;
            DateTime firstDayOfLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            DateTime lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);

            // 查询每个员工上个月做的手术台数
            var surgeryCounts = await DbContext.db.Queryable<OPERATE>()
                .Where(o => o.FOUND_DATE >= firstDayOfLastMonth && o.FOUND_DATE <= lastDayOfLastMonth)
                .GroupBy(o => o.SER_ID)
                .Select(o => new
                {
                    SER_ID = o.SER_ID,
                    SurgeryCount = SqlFunc.AggregateCount(o.SER_ID)
                })
                .OrderByDescending(o => o.SurgeryCount)
                .ToListAsync();

            // 找出做手术台数最多的员工
            int maxSurgeryCount = surgeryCounts.First().SurgeryCount;
            var topPerformers = surgeryCounts.Where(s => s.SurgeryCount == maxSurgeryCount).ToList();

            // 给他们加工资（例如，加1000元）
            foreach (var performer in topPerformers)
            {
                await DbContext.db.Updateable<SERVER>()
                    .SetColumns(s => new SERVER { TAKEHOMEPAY = s.TAKEHOMEPAY + 1000 })
                    .Where(s => s.SER_ID == performer.SER_ID)
                    .ExecuteCommandAsync();
            }
        }
    }
}
