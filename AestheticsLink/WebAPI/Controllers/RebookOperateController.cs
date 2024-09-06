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
                ReturnDto returnDto = _rebookService.PostponeOperate(opearte);
                if (returnDto != null)
                {
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
