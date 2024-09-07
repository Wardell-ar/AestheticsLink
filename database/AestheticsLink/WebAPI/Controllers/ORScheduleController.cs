using LogRegService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORScheduleService;
using QueryAllUsersService.QueryAllCustomers;
using WebCommon.Database;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class ORScheduleController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IO_RScheduleService _o_rScheduleService;

        public ORScheduleController(IO_RScheduleService o_rScheduleService, ILogger<LoginController> logger)
        {
            _o_rScheduleService = o_rScheduleService;
            _logger = logger;
        }

        [HttpPost("QueryO_RSchedule")]
        public async Task<IActionResult> QueryO_RSchedule([FromBody] Dictionary<string, object> criteria)
        {
            _logger.LogInformation("Received O_RSchedule request!");
            try
            {
                DbContext.db.Ado.BeginTran();
                var o_rschedule = await _o_rScheduleService.GetO_RScheduleByCriteria(criteria);
                if (o_rschedule != null && o_rschedule.Count != 0)
                {
                    DbContext.db.Ado.CommitTran();
                    return Ok(o_rschedule);
                }
                else
                {
                    DbContext.db.Ado.RollbackTran();
                    return Ok("0");
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
