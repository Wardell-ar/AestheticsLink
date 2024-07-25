using Microsoft.AspNetCore.Mvc;
using RechargeService;
using RechargeService.Dto;
using WebCommon.Database;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]

    public class RechargeController : ControllerBase
    {
        private readonly IRechargeService _rechargeService;

        public RechargeController(IRechargeService rechargeService)
        {
            _rechargeService = rechargeService;
        }
        [HttpPost("Recharge")]
        public IActionResult PostRecharge(RechargeDto request)
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();
                if (_rechargeService.CustomerRecharge(request))
                {
                    //事物提交
                    DbContext.db.Ado.CommitTran();
                    return Ok();
                }
                else
                {
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest("顾客信息有误");
                }
            }
            catch (Exception ex) 
            {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, ex.Message);
            }
        }
    }
}
