using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Presentation;
using OperateService.Dto;
using RebookOperateService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace RebookOperateService
{
    public class RebookService : IRebookService
    {
        public ReturnDto PostponeOperate(DelayedOperateDto delayedOperate)
        {
            try
            {
                ReturnDto returnDto = new ReturnDto();
                //判断cusid和projid是否对应
                var isMatch = DbContext.db.Ado.SqlQuerySingle<string>("SELECT CUS_ID FROM BILL WHERE CUS_ID = :cus AND BILL_ID = :bill", new { cus = delayedOperate.customerId, bill = delayedOperate.billid });
                if (isMatch == null) { return null; }
                var projid = DbContext.db.Ado.SqlQuerySingle<string>(
                    "SELECT PROJ_ID FROM PROJECT WHERE NAME = :name ",
                    new
                    {
                        name = delayedOperate.operationName,
                    });

                //找到需要修改的项目
                var operate = DbContext.db.Ado.SqlQuerySingle<OPERATE>(
                    "SELECT * FROM OPERATE WHERE BILL_ID = :billId AND PROJ_ID = :projId ",
                    new
                    {
                        billId = delayedOperate.billid,
                        projId = projid
                    });
                var operateTime = DbContext.db.Ado.SqlQuerySingle<OPERATE_TIME>(
                    "SELECT * FROM OPERATE_TIME WHERE OP_TIME_ID = :timeID ",
                    new
                    {
                        timeID = operate.OP_TIME_ID,
                    });
                //找到对应医院
                var Hos_Id = DbContext.db.Ado.SqlQuerySingle<string>(
                    "SELECT HOS_ID FROM BILL WHERE BILL_ID = :billId",
                    new
                    {
                        billId = delayedOperate.billid,
                    });

                //找到可用的时间和手术室,从当前时间往后找找到第一个可用时间
                var availableTimes = DbContext.db.Ado.SqlQuery<OperateTimeDto>(
                    "SELECT * FROM OPERATE_TIME NATURAL JOIN OPERATING_ROOM WHERE STATUS = :status AND START_TIME > :time AND DAY >= :day AND HOS_ID = :hos ORDER BY START_TIME ASC",
                    new
                    {
                        status = "0",
                        time = operateTime.START_TIME,
                        day = operateTime.DAY,
                        hos = Hos_Id,
                    });
                // 查找客户已经安排的手术时间段
                var scheduledTimes = DbContext.db.Ado.SqlQuery<OperateTimeDto>(
                    "SELECT START_TIME, END_TIME FROM OPERATE_TIME NATURAL JOIN OPERATE NATURAL JOIN BILL WHERE CUS_ID = :customerId AND DAY = :day",
                    new
                    {
                        customerId = delayedOperate.customerId,
                        day = operateTime.DAY,
                    });
                foreach (var availableTime in availableTimes)
                {
                    // 检查当前可用时间段是否与客户的已安排手术时间段冲突
                    bool hasConflict = scheduledTimes.Any(st =>
                    availableTime.START_TIME < st.END_TIME && availableTime.END_TIME > st.START_TIME);
                    if (!hasConflict)
                    {
                        //找到在可用时间可用的医生
                        string hosID = DbContext.db.Ado.SqlQuerySingle<string>("SELECT HOS_ID FROM BILL WHERE BILL_ID = :bill", new { bill = delayedOperate.billid });
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
                                    status = "1",
                                    time = availableTime.START_TIME,
                                    day = availableTime.DAY,
                                    hos = hosID,
                                    pos = "医生",
                                });
                        DbContext.db.Ado.ExecuteCommand(
                       "UPDATE OPERATE_TIME SET STATUS = :status WHERE OP_TIME_ID = :timeID",
                       new
                       {
                           status = "0",
                           timeID = operateTime.OP_TIME_ID,
                       });
                        DbContext.db.Ado.ExecuteCommand(
                        "UPDATE OPERATE SET SER_ID = :serID WHERE OP_TIME_ID = :timeID",
                        new
                        {
                            serID = availableDoctors[0].SER_ID,
                            timeID = availableTime.OP_TIME_ID,
                        });
                        DbContext.db.Ado.ExecuteCommand(
                        "UPDATE OPERATE SET OP_TIME_ID = :timeID WHERE BILL_ID = :billId AND PROJ_ID = :projId",
                        new
                        {
                            timeID = availableTime.OP_TIME_ID,
                            billId = delayedOperate.billid,
                            projId = projid
                        });

                        DbContext.db.Ado.ExecuteCommand(
                        "UPDATE OPERATE_TIME SET STATUS = :status WHERE OP_TIME_ID = :timeID",
                        new
                        {
                            status = "1",
                            timeID = availableTime.OP_TIME_ID,
                        });
                        returnDto.day = availableTime.DAY.Day.ToString();
                        returnDto.month = availableTime.DAY.Month.ToString();
                        returnDto.year = availableTime.DAY.Year.ToString();
                        returnDto.startTime = availableTime.START_TIME.ToString();
                        returnDto.endTime = availableTime.END_TIME.ToString();
                        break;
                    }
                }
                if (returnDto != null)
                    return returnDto;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
