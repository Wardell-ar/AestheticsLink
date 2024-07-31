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
        public async Task<IActionResult> QueryCustomers([FromBody] Dictionary<string, object> criteria)
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
                    return Ok("0");
                }
            }
            catch (Exception ex) {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("DeleteCustomers")]
        public async Task<IActionResult> DeleteCustomers([FromBody] DeleteCustomersDto deleteCustomersDto)
        {
            _logger.LogInformation("Received DeleteCustomers request!");

            // 提取cus_id列表
            var cusIds = deleteCustomersDto.cus_ids;


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
                    // 尝试删除 CUS_COU 表中相关记录（即使没有找到匹配的记录，也不会中断）
                    await DbContext.db.Deleteable<CUS_COU>()
                                      .Where(c => cusIds.Contains(c.CUS_ID))
                                      .ExecuteCommandAsync();

                    var result = await DbContext.db.Deleteable<CUSTOMER>()
                                                  .In(cusIds)
                                                  .ExecuteCommandAsync();
                    if (result > 0)
                    {
                        //事务提交
                        DbContext.db.Ado.CommitTran();
                        return Ok("1");
                    }
                    else
                    {
                        // 事务回滚
                        DbContext.db.Ado.RollbackTran();
                        return Ok("0");
                    }
                }
                else
                {
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return Ok("0");
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
