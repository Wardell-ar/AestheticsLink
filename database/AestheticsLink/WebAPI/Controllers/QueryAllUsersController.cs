using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueryAllUsersService.QueryAllCustomers;
using QueryAllUsersService.QueryAllCustomers.Dto;
using QueryAllUsersService.QueryAllServers;
using QueryAllUsersService.QueryAllServers.Dto;
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
        private readonly IQueryAllServersService _queryAllServersService;

        public QueryAllUsersController(IQueryAllCustomersService queryAllCustomersService, IQueryAllServersService queryAllServersService, ILogger<LoginController> logger)
        {
            _queryAllCustomersService = queryAllCustomersService;
            _queryAllServersService = queryAllServersService;
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


        [HttpPost("QueryServers")]
        public async Task<IActionResult> QueryServers([FromBody] Dictionary<string, object> criteria)
        {
            _logger.LogInformation("Received QueryServers request!");
            try
            {
                DbContext.db.Ado.BeginTran();
                var servers = await _queryAllServersService.GetServersByCriteria(criteria);
                if (servers != null && servers.Count != 0)
                {
                    DbContext.db.Ado.CommitTran();
                    return Ok(servers);
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

        [HttpPost("DeleteServers")]
        public async Task<IActionResult> DeleteServers([FromBody] DeleteServersDto deleteServersDto)
        {
            _logger.LogInformation("Received DeleteServers request!");

            // 提取 ser_id 列表
            var serIds = deleteServersDto.ser_ids;


            // 删除数据库中的对应员工信息
            try
            {
                DbContext.db.Ado.BeginTran();

                // 查询数据库中是否存在所有的 ser_id
                var existingSerIds = await DbContext.db.Queryable<SERVER>()
                                                       .Where(s => serIds.Contains(s.SER_ID))
                                                       .Select(s => s.SER_ID)
                                                       .ToListAsync();

                if (existingSerIds.Count == serIds.Count)
                {
                    // 检查哪些员工有手术记录
                    var operations = await DbContext.db.Queryable<OPERATE>()
                                                       .In(op => op.SER_ID, serIds)
                                                       .ToListAsync();

                    if (operations.Count > 0)
                    {
                        // 将这些手术记录的SER_ID设置为null
                        await DbContext.db.Updateable<OPERATE>()
                                          .SetColumns(op => new OPERATE { SER_ID = "" })
                                          .Where(op => serIds.Contains(op.SER_ID))
                                          .ExecuteCommandAsync();
                    }

                    var result = await DbContext.db.Deleteable<SERVER>()
                                                  .In(s => s.SER_ID, serIds)
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
                _logger.LogError(ex, "删除员工信息时出错");
                return StatusCode(500, "服务器内部错误");
            }
        }

        [HttpPost("UpdateServers")]
        public async Task<IActionResult> UpdateServers([FromBody] UpdateServersDto updateServersDto)
        {
            _logger.LogInformation("Received UpdateServers request!");
            try
            {
                DbContext.db.Ado.BeginTran();

                // 检查ser_id是否存在
                var existingServer = await DbContext.db.Queryable<SERVER>()
                    .Where(s => s.SER_ID == updateServersDto.ser_id)
                    .SingleAsync();

                if (existingServer == null)
                {
                    _logger.LogWarning($"Server with SER_ID: {updateServersDto.ser_id} does not exist.");
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest("查无此员工");
                }

                // 更新salary字段
                if (updateServersDto.salary >= 0)
                {
                    existingServer.BASICSALARY = updateServersDto.salary;
                }

                // 更新position字段
                if (!string.IsNullOrEmpty(updateServersDto.position))
                {
                    existingServer.POSITION = updateServersDto.position;
                }

                // 更新 hos_id 和 dep_id
                if (!string.IsNullOrEmpty(updateServersDto.hos_name))
                {
                    var hos = await DbContext.db.Queryable<HOSPITAL>()
                        .Where(h => h.NAME == updateServersDto.hos_name)
                        .SingleAsync();

                    if (hos != null)
                    {
                        existingServer.HOS_ID = hos.HOS_ID;
                    }
                    else
                    {
                        DbContext.db.Ado.RollbackTran();
                        return BadRequest("查无此医院!");
                    }
                }
                if (!string.IsNullOrEmpty(updateServersDto.dep_name))
                {
                    var dep = await DbContext.db.Queryable<DEPARTMENT>()
                        .Where(d => d.NAME == updateServersDto.dep_name)
                        .SingleAsync();

                    if (dep != null)
                    {
                        existingServer.DEP_ID = dep.DEP_ID;
                    }
                    else
                    {
                        DbContext.db.Ado.RollbackTran();
                        return BadRequest("查无此部门!");
                    }
                }
                // 更新SERVER表中的记录
                await DbContext.db.Updateable(existingServer)
                    .ExecuteCommandAsync();

                DbContext.db.Ado.CommitTran();
                return Ok("1");
            }
            catch (Exception ex)
            {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateServer")]
        public async Task<IActionResult> CreateServer([FromBody] CreateServerDto createServerDto)
        {
            _logger.LogInformation("Received CreateServer request!");
            try
            {
                DbContext.db.Ado.BeginTran();

                // 检查是否有重复的 SER_ID
                var existingServer = await DbContext.db.Queryable<SERVER>()
                    .Where(s => s.SER_ID == createServerDto.ser_id)
                    .FirstAsync();

                if (existingServer != null)
                {
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest("员工ID已存在，无法创建新员工。");
                }

                // 获取 HOS_ID
                var hosId = await DbContext.db.Queryable<HOSPITAL>()
                    .Where(h => h.NAME == createServerDto.hos_name)
                    .Select(h => h.HOS_ID)
                    .FirstAsync();

                if (hosId == null)
                {
                    // 如果没有找到医院，回滚事务并返回错误
                    DbContext.db.Ado.RollbackTran();
                    return StatusCode(400, "查无此医院，无法创建新员工。");
                }

                // 获取 DEP_ID
                var depId = await DbContext.db.Queryable<DEPARTMENT>()
                    .Where(d => d.NAME == createServerDto.dep_name)
                    .Select(d => d.DEP_ID)
                    .FirstAsync();

                if (depId == null)
                {
                    // 如果没有找到部门，回滚事务并返回错误
                    DbContext.db.Ado.RollbackTran();
                    return StatusCode(400, "查无此部门，无法创建新员工。");
                }

                // 检查医院是否有此部门
                var isDepartmentInHospital = await DbContext.db.Queryable<HOS_DEP>()
                    .Where(hd => hd.H_ID == hosId && hd.D_ID == depId)
                    .AnyAsync();

                if (!isDepartmentInHospital)
                {
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest("在指定医院中未找到指定部门，无法创建新员工。");
                }

                // 验证work_time_id是否正确
                var workTimeExists = await DbContext.db.Queryable<WORK_TIME>()
                    .Where(w => w.WORK_TIME_ID == createServerDto.work_time_id)
                    .AnyAsync();

                if (!workTimeExists)
                {
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest("无效的工作时间ID，无法创建新员工。");
                }



                // 添加新员工、设置默认值
                var newServer = new SERVER
                {
                    SER_ID = createServerDto.ser_id,
                    JOINED_DATE = createServerDto.joined_date,
                    NAME = createServerDto.name,
                    PASSWORD = createServerDto.ser_id, // 默认密码为 SER_ID
                    POSITION = createServerDto.position,
                    PHONE_NUM = createServerDto.phone_num,
                    BASICSALARY = createServerDto.salary,
                    TAKEHOMEPAY = createServerDto.salary, // TAKEHOMEPAY 默认等于 BASICSALARY
                    HOS_ID = hosId,
                    DEP_ID = depId,
                    BIRTHDAY = createServerDto.birthday,
                    WORK_TIME_ID = createServerDto.work_time_id,
                    IS_WORK_TODAY = 0, // 默认值为 0
                    GENDER = createServerDto.gender
                };

                // 插入新员工记录
                await DbContext.db.Insertable(newServer).ExecuteCommandAsync();

                DbContext.db.Ado.CommitTran();
                return Ok("1");
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
