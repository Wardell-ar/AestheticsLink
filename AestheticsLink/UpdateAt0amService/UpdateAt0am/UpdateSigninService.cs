using Quartz;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace UpdateAt0amService.UpdateAt0am
{
    public class UpdateSigninService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await UpdateSignin();
        }

        public async Task UpdateSignin()
        {
            var today = (int)DateTime.Now.DayOfWeek; // 获取当前计算机对应的星期(0=星期日;1=星期一;...)
            await Task.Run(() =>
            {
                if ((today + 6) % 7 == 0)
                {
                    DbContext.db.Updateable<SERVER>()
                        .SetColumns(it => new SERVER { TAKEHOMEPAY = it.TAKEHOMEPAY - 50 })
                        .Where(it => it.IS_WORK_TODAY == 0)
                        .Where(it => SqlFunc.Subqueryable<WORK_TIME>().Where(w => w.WORK_TIME_ID == it.WORK_TIME_ID && w.DAY7 == 1).Any())
                        .ExecuteCommand();
                }
                else if ((today + 6) % 7 == 1)
                {
                    DbContext.db.Updateable<SERVER>()
                        .SetColumns(it => new SERVER { TAKEHOMEPAY = it.TAKEHOMEPAY - 50 })
                        .Where(it => it.IS_WORK_TODAY == 0)
                        .Where(it => SqlFunc.Subqueryable<WORK_TIME>().Where(w => w.WORK_TIME_ID == it.WORK_TIME_ID && w.DAY1 == 1).Any())
                        .ExecuteCommand();
                }
                else if ((today + 6) % 7 == 2)
                {
                    DbContext.db.Updateable<SERVER>()
                        .SetColumns(it => new SERVER { TAKEHOMEPAY = it.TAKEHOMEPAY - 50 })
                        .Where(it => it.IS_WORK_TODAY == 0)
                        .Where(it => SqlFunc.Subqueryable<WORK_TIME>().Where(w => w.WORK_TIME_ID == it.WORK_TIME_ID && w.DAY2 == 1).Any())
                        .ExecuteCommand();
                }
                else if ((today + 6) % 7 == 3)
                {
                    DbContext.db.Updateable<SERVER>()
                        .SetColumns(it => new SERVER { TAKEHOMEPAY = it.TAKEHOMEPAY - 50 })
                        .Where(it => it.IS_WORK_TODAY == 0)
                        .Where(it => SqlFunc.Subqueryable<WORK_TIME>().Where(w => w.WORK_TIME_ID == it.WORK_TIME_ID && w.DAY3 == 1).Any())
                        .ExecuteCommand();
                }
                else if ((today + 6) % 7 == 4)
                {
                    DbContext.db.Updateable<SERVER>()
                        .SetColumns(it => new SERVER { TAKEHOMEPAY = it.TAKEHOMEPAY - 50 })
                        .Where(it => it.IS_WORK_TODAY == 0)
                        .Where(it => SqlFunc.Subqueryable<WORK_TIME>().Where(w => w.WORK_TIME_ID == it.WORK_TIME_ID && w.DAY4 == 1).Any())
                        .ExecuteCommand();
                }
                else if ((today + 6) % 7 == 5)
                {
                    DbContext.db.Updateable<SERVER>()
                        .SetColumns(it => new SERVER { TAKEHOMEPAY = it.TAKEHOMEPAY - 50 })
                        .Where(it => it.IS_WORK_TODAY == 0)
                        .Where(it => SqlFunc.Subqueryable<WORK_TIME>().Where(w => w.WORK_TIME_ID == it.WORK_TIME_ID && w.DAY5 == 1).Any())
                        .ExecuteCommand();
                }
                else if ((today + 6) % 7 == 6)
                {
                    DbContext.db.Updateable<SERVER>()
                        .SetColumns(it => new SERVER { TAKEHOMEPAY = it.TAKEHOMEPAY - 50 })
                        .Where(it => it.IS_WORK_TODAY == 0)
                        .Where(it => SqlFunc.Subqueryable<WORK_TIME>().Where(w => w.WORK_TIME_ID == it.WORK_TIME_ID && w.DAY6 == 1).Any())
                        .ExecuteCommand();
                }

                DbContext.db.Updateable<SERVER>()
                    .SetColumns(it => new SERVER() { IS_WORK_TODAY = 0 })
                    .Where(it => it.IS_WORK_TODAY == 1)
                    .ExecuteCommand();
            });
        }
    }
}
