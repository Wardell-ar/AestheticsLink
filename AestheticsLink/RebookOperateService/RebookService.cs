using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
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
        public bool PostponeOperate(DelayedOperateDto delayedOperate)
        {
            try
            {
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
                //找到可用的时间和手术室,从当前时间往后找找到第一个可用时间
                var availableTimes = DbContext.db.Ado.SqlQuery<OperateTimeDto>(
                    "SELECT * FROM OPERATE_TIME WHERE STATUS = :status AND DAY > :day ",
                    new
                    {
                        status = "0",
                        day = operateTime.DAY
                    });
                int i = 0;

                DbContext.db.Ado.ExecuteCommand(
               "UPDATE OPERATE SET OP_TIME_ID = :timeID WHERE BILL_ID = :billId AND PROJ_ID = :projId",
               new
               {
                   timeID = availableTimes[i].OP_TIME_ID,
                   billId = delayedOperate.billid,
                   projId = projid
               });

                DbContext.db.Ado.ExecuteCommand(
                "UPDATE OPERATE_TIME SET STATUS = :status WHERE OP_TIME_ID = :timeID",
                new
                {
                    status = "1",
                    timeID = availableTimes[i].OP_TIME_ID,
                });

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
                              "AND HOS_ID = :hos",
                        new
                        {
                            status = "1",
                            time = availableTimes[i].START_TIME,
                            day = availableTimes[i].DAY,
                            hos = hosID,
                        });
                DbContext.db.Ado.ExecuteCommand(
                "UPDATE OPERATE SET SER_ID = :serID WHERE OP_TIME_ID = :timeID",
                new
                {
                    serID = availableDoctors[i].SER_ID,
                    timeID = availableTimes[i].OP_TIME_ID,
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
