using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;

namespace TimeService
{
    [DisallowConcurrentExecution]
    public class TimeAddService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await AddTime();
        }

        private async Task AddTime()
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();
                //// 获取下周的第一天
                //DateTime nextWeekStart = DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek + 1);

                //// 每天的四个时间段
                //TimeSpan[] times = {
                //    new TimeSpan(8, 0, 0),   // 08:00
                //    new TimeSpan(10, 0, 0),  // 10:00
                //    new TimeSpan(14, 0, 0),  // 13:00
                //    new TimeSpan(16, 0, 0)   // 15:00
                //};

                //// 遍历一周的每一天
                //for (int i = 0; i < 7; i++)
                //{
                //    DateTime currentDay = nextWeekStart.AddDays(i);

                //    foreach (var roomId in GetAllRoomIds()) // 假设你有一个方法获取所有手术室的ID
                //    {
                //        foreach (var timeSlot in times)
                //        {
                //            DateTime startTime = currentDay + timeSlot;
                //            DateTime endTime = startTime.AddHours(2); // 假设每个手术持续2小时

                //            // 生成新的 OP_TIME_ID，假设你有一个生成ID的方法
                //            string newOpTimeId = (int.Parse(GenerateNewOpTimeId()) + 1).ToString();

                //            // 插入新的operate_time记录
                //            DbContext.db.Ado.ExecuteCommand(
                //                "INSERT INTO OPERATE_TIME (OP_TIME_ID, ROOM_ID, START_TIME, END_TIME, STATUS, DAY) " +
                //                "VALUES (@opTimeId, @roomId, @startTime, @endTime, @status, @day)",
                //                new
                //                {
                //                    opTimeId = newOpTimeId,
                //                    roomId = roomId,
                //                    startTime = startTime,
                //                    endTime = endTime,
                //                    status = "0",
                //                    day = currentDay
                //                });
                //        }
                //    }
                //}
                // 获取明天和本周的最后一天（周日）
                DateTime tomorrow = DateTime.Today.AddDays(1);
                DateTime thisSunday = DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek);

                // 每天的四个时间段
                TimeSpan[] timeSlots = {
            new TimeSpan(8, 0, 0),   // 08:00
            new TimeSpan(10, 0, 0),  // 10:00
            new TimeSpan(14, 0, 0),  // 13:00
            new TimeSpan(16, 0, 0)   // 15:00
        };

                // 遍历从明天到周天的每一天
                for (DateTime day = tomorrow; day <= thisSunday; day = day.AddDays(1))
                {
                    foreach (var roomId in GetAllRoomIds()) // 获取所有手术室ID
                    {
                        foreach (var timeSlot in timeSlots)
                        {
                            DateTime startTime = day + timeSlot;
                            DateTime endTime = startTime.AddHours(2); // 假设每个手术持续2小时

                            // 生成新的 OP_TIME_ID，可以使用 Guid 或其他方式生成唯一ID
                            string newOpTimeId = (int.Parse(GenerateNewOpTimeId()) + 1).ToString();

                            // 插入新的 operate_time 记录
                            DbContext.db.Ado.ExecuteCommand(
                                "INSERT INTO OPERATE_TIME (OP_TIME_ID, ROOM_ID, START_TIME, END_TIME, STATUS, DAY) " +
                                "VALUES (@opTimeId, @roomId, @startTime, @endTime, @status, @day)",
                                new
                                {
                                    opTimeId = newOpTimeId,
                                    roomId = roomId,
                                    startTime = startTime,
                                    endTime = endTime,
                                    status = "0", // 未占用
                                    day = day
                                });
                        }
                    }
                }

                //事物提交
                DbContext.db.Ado.CommitTran();

            }
            catch (Exception ex)
            {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                throw ex;
            }
        }
        // 生成新的OP_TIME_ID的函数
        private string GenerateNewOpTimeId()
        {
            string sql = "SELECT MAX(TO_NUMBER(OP_TIME_ID)) FROM OPERATE_TIME";

            // 执行 SQL 查询
            var result = DbContext.db.Ado.GetString(sql);

            return result;
        }

        // 获取所有手术室ID的函数，返回手术室ID列表
        private List<string> GetAllRoomIds()
        {
            return DbContext.db.Ado.SqlQuery<string>("SELECT ROOM_ID FROM OPERATING_ROOM"); // 假设 Room 是手术室的表
        }
    }

}
