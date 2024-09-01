using Microsoft.AspNetCore.Mvc;
using RebookOperateService;
using RebookOperateService.Dto;
using WebCommon.Database;
using WebModel.Entity;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]

    public class RebookOperateController : ControllerBase
    {
        private readonly IRebookService _rebookService;
        public RebookOperateController(IRebookService rebookService)
        {
            _rebookService = rebookService;
        }

        [HttpPost("DalayOperate")]
        public IActionResult DalayOperate([FromBody] DelayedOperateDto opearte)
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();

                if (_rebookService.PostponeOperate(opearte))
                {
                    var projid = DbContext.db.Ado.SqlQuerySingle<string>(
                        "SELECT PROJ_ID FROM PROJECT WHERE NAME = :name ",
                        new
                        {
                            name = opearte.operationName,
                        });
                    ReturnDto returnDto = new ReturnDto();
                    //查找OPERATE_TIME
                    var time = DbContext.db.Ado.SqlQuerySingle<OPERATE_TIME>(
                        "SELECT * FROM OPERATE_TIME NATURAL JOIN OPERATE WHERE BILL_ID = :billId AND PROJ_ID = :projId ",
                        new
                        {
                            billId = opearte.billid,
                            projId = projid
                        });

                    returnDto.startTime = time.START_TIME.ToLongTimeString();
                    returnDto.endTime = time.END_TIME.ToLongTimeString();
                    returnDto.year = time.DAY.Year.ToString();
                    returnDto.month = time.DAY.Month.ToString();
                    returnDto.day = time.DAY.Day.ToString();
                    //事物提交
                    DbContext.db.Ado.CommitTran();

                    return Ok(returnDto);
                }
                else
                {
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return Ok("0");
                }
            }
            catch
            {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return Ok("0");
            }
        }


    }
}
