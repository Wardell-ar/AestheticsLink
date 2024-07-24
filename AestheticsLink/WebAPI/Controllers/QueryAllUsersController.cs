using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueryAllUsersService.QueryAllCustomers;
using QueryAllUsersService.QueryAllCustomers.Dto;
using ServerInformation.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class QueryAllUsersController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IQueryAllCustomersService _queryAllCustomersService;

        public QueryAllUsersController(IQueryAllCustomersService queryAllCustomersService, ILogger<LoginController> logger)
        {
            _queryAllCustomersService = queryAllCustomersService;
            _logger = logger;
        }

        [HttpPost("QueryCustomers")]
        public async Task<ActionResult<List<QueryCustomersDto>>> QueryCustomers([FromBody] Dictionary<string, object> criteria)
        {
            _logger.LogInformation("Received QueryCustomers request!");

            var customers = await _queryAllCustomersService.GetCustomersByCriteria(criteria);
            if (customers == null || customers.Count == 0)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        [HttpPost("DeleteCustomers")]
        public async Task<IActionResult> DeleteCustomers([FromBody] List<DeleteCustomersDto> deleteCustomersDto)
        {
            _logger.LogInformation("Received DeleteCustomers request!");

            // 提取cus_id列表
            var cusIds = deleteCustomersDto.Select(dto => dto.cus_id).ToList();

            // 删除数据库中的对应顾客信息
            try
            {
                var result = await DbContext.db.Deleteable<CUSTOMER>()
                                      .In(cusIds)
                                      .ExecuteCommandAsync();

                if (result > 0)
                {
                    return Ok("删除成功");
                }
                else
                {
                    return NotFound("未找到要删除的顾客");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除顾客信息时出错");
                return StatusCode(500, "服务器内部错误");
            }
        }
    }
}
