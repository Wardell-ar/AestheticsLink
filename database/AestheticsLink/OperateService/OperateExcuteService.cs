using Microsoft.Extensions.Logging;
using OperateService.Dto;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace OperateService
{
    [DisallowConcurrentExecution]
    public class OperateExcuteService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await OperateExecute();
        }

        private async Task OperateExecute()
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();

                var day = DateTime.Now;
                //找到需要执行的手术
                var operateToUpdate = await DbContext.db.Ado.SqlQuerySingleAsync<OperateDto>(
                    "SELECT PROJ_ID, BILL_ID, OP_TIME_ID " +
                    "FROM OPERATE NATURAL JOIN OPERATE_TIME " +
                    "WHERE DAY = :day AND END_TIME <= :time AND EXE_STATE = :state",
                    new
                    {
                        day = day.Date,
                        time = day,
                        state = "0"
                    });
                if (operateToUpdate != null)
                {
                    //将该手术置为已完成
                    await DbContext.db.Ado.ExecuteCommandAsync(
                        "UPDATE OPERATE " +
                        "SET EXE_STATE = :state " +
                        "WHERE PROJ_ID = :projID AND BILL_ID = :billID AND OP_TIME_ID = :timeID",
                        new
                        {
                            state = "1",
                            projID = operateToUpdate.PROJ_ID,
                            billID = operateToUpdate.BILL_ID,
                            timeID = operateToUpdate.OP_TIME_ID,
                        });
                    //扣除药品
                    var goodID = await DbContext.db.Ado.SqlQueryAsync<string>(
                        "SELECT G_ID FROM OPERATE NATURAL JOIN PROJ_GOOD WHERE PROJ_ID = :projID AND BILL_ID = :billID",
                        new
                        {
                            projID = operateToUpdate.PROJ_ID,
                            billID = operateToUpdate.BILL_ID,
                        });
                    foreach (var item in goodID)
                    {
                        await DbContext.db.Ado.ExecuteCommandAsync(
                            "UPDATE INVENTORY " +
                            "SET STORAGE = STORAGE - :storage " +
                            "WHERE G_ID = :gID",
                            new
                            {
                                storage = 1,
                                gID = item,
                            });
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
    }
}
