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
            try
            {
                DbContext.db.Ado.BeginTran();
                var customers = await _queryAllCustomersService.GetCustomersByCriteria(criteria);
                if (customers != null && customers.Count != 0)
                {
                    DbContext.db.Ado.CommitTran();
                    return Ok(customers);
                }
                else
                {
                    DbContext.db.Ado.RollbackTran();
                    return NotFound("未找到匹配条件的顾客");
                }
            }
            catch (Exception ex) {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("DeleteCustomers")]
        public async Task<IActionResult> DeleteCustomers([FromBody] List<string> deleteCustomersDto)
        {
            _logger.LogInformation("Received DeleteCustomers request!");

            // 提取cus_id列表
            var cusIds = deleteCustomersDto;


            // 删除数据库中的对应顾客信息
            try
            {
                DbContext.db.Ado.BeginTran();

                // 查询数据库中是否存在所有的 cus_id
                var existingCusIds = await DbContext.db.Queryable<CUSTOMER>()
                                                       .Where(c => cusIds.Contains(c.CUS_ID))
                                                       .Select(c => c.CUS_ID)
                                                       .ToListAsync();
                if (existingCusIds.Count == cusIds.Count)
                {
                    var result = await DbContext.db.Deleteable<CUSTOMER>()
                                          .In(cusIds)
                                          .ExecuteCommandAsync();
                    if (result > 0)
                    {
                        DbContext.db.Ado.CommitTran();
                        return Ok("删除成功");
                    }
                    else
                    {
                        //事物回滚
                        DbContext.db.Ado.RollbackTran();
                        return NotFound("删除顾客信息时出错");
                    }
                }
                else
                {
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return NotFound("未找到要删除的顾客");
                }
            }
            catch (Exception ex)
            {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                _logger.LogError(ex, "删除顾客信息时出错");
                return StatusCode(500, "服务器内部错误");
            }
        }
    }
}
