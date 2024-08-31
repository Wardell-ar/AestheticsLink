using HosAndDepService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HosAndDepService.Dto;
using WebCommon.Database;
using WebModel.Entity;
using Microsoft.CodeAnalysis.Operations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class HosAndDepController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IHosAndDepInfoService _hosAndDepInfoService;

        public HosAndDepController(IHosAndDepInfoService hosAndDepInfoService, ILogger<LoginController> logger)
        {
            _hosAndDepInfoService = hosAndDepInfoService;
            _logger = logger;
        }

        [HttpPost("QueryHosAndDepInfo")]
        public async Task<IActionResult> QueryHosAndDepInfoAsync([FromBody] Dictionary<string, object> criteria)
        {
            _logger.LogInformation("Received QueryHosAndDepInfo request!");
            try
            {
                DbContext.db.Ado.BeginTran();
                var HosAndDepresponse = await _hosAndDepInfoService.GetHosAndDepInfoByCriteria(criteria);
                if (HosAndDepresponse != null && HosAndDepresponse.Count != 0)
                {
                    DbContext.db.Ado.CommitTran();
                    return Ok(HosAndDepresponse);
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

        [HttpPost("DeleteHosAndDepInfo")]
        public async Task<IActionResult> DeleteHosAndDepInfoAsync([FromBody] DeleteHADInfoReceptionDto deleteHADInfoReceptionDto)
        {
            _logger.LogInformation("Received DeleteHosAndDepInfo request!");

            try
            {
                DbContext.db.Ado.BeginTran();

                string hosId = deleteHADInfoReceptionDto.hosId;
                string depId = deleteHADInfoReceptionDto.branchId;

                // 检查hos_id是否存在
                var hospitalExists = await DbContext.db.Queryable<HOSPITAL>()
                                                       .AnyAsync(h => h.HOS_ID == hosId);
                if (!hospitalExists)
                {
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest("医院ID不存在！");
                }

                // 检查dep_id是否存在
                var departmentExists = await DbContext.db.Queryable<DEPARTMENT>()
                                                         .AnyAsync(d => d.DEP_ID == depId);
                if (!departmentExists)
                {
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest("部门ID不存在！");
                }

                // 检查HOS_DEP表中是否存在相应的hos_id和dep_id元组
                var hosDepExists = await DbContext.db.Queryable<HOS_DEP>()
                    .AnyAsync(hd => hd.H_ID == hosId && hd.D_ID == depId);

                if (!hosDepExists)
                {
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest("该医院已没有此部门。");
                }


                // 获取该部门中的所有员工的SER_ID
                var serIds = await DbContext.db.Queryable<SERVER>()
                                               .Where(s => s.DEP_ID == depId)
                                               .Select(s => s.SER_ID)
                                               .ToListAsync();

                // 将OPERATE表中对应的SER_ID字段置为空字符串
                await DbContext.db.Updateable<OPERATE>()
                                 .SetColumns(o => o.SER_ID == "")
                                 .Where(o => serIds.Contains(o.SER_ID))
                                 .ExecuteCommandAsync();

                // 删除该部门中的所有员工
                await DbContext.db.Deleteable<SERVER>()
                                 .Where(s => s.DEP_ID == depId)
                                 .ExecuteCommandAsync();

                // 删除hos_dep表中的记录
                await DbContext.db.Deleteable<HOS_DEP>()
                                  .Where(hd => hd.H_ID == hosId && hd.D_ID == depId)
                                  .ExecuteCommandAsync();

                DbContext.db.Ado.CommitTran();
                return Ok("1");
            }
            catch (Exception ex)
            {
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("BatchDeleteHosAndDepInfo")]
        public async Task<IActionResult> BatchDeleteHosAndDepInfoAsync([FromBody] List<DeleteHADInfoReceptionDto> deleteHADInfoReceptionDtoList)
        {
            _logger.LogInformation("Received BatchDeleteHosAndDepInfo request!");

            try
            {
                DbContext.db.Ado.BeginTran();

                foreach (var deleteDto in deleteHADInfoReceptionDtoList)
                {
                    // 检查hos_id和dep_id是否存在
                    bool hosDepExists = await DbContext.db.Queryable<HOS_DEP>()
                        .AnyAsync(hd => hd.H_ID == deleteDto.hosId && hd.D_ID == deleteDto.branchId);

                    if (!hosDepExists)
                    {
                        DbContext.db.Ado.RollbackTran();
                        return BadRequest("该医院已没有此部门。");
                    }

                    // 获取将要删除的部门的所有员工的SER_ID
                    var serIds = await DbContext.db.Queryable<SERVER>()
                        .Where(s => s.DEP_ID == deleteDto.branchId)
                        .Select(s => s.SER_ID)
                        .ToListAsync();

                    // 将OPERATE表中这些员工的SER_ID字段置为空字符串
                    await DbContext.db.Updateable<OPERATE>()
                        .SetColumns(op => new OPERATE { SER_ID = string.Empty })
                        .Where(op => serIds.Contains(op.SER_ID))
                        .ExecuteCommandAsync();

                    // 删除该部门的所有员工
                    await DbContext.db.Deleteable<SERVER>()
                        .Where(s => s.DEP_ID == deleteDto.branchId)
                        .ExecuteCommandAsync();

                    // 删除hos_dep表中的记录
                    await DbContext.db.Deleteable<HOS_DEP>()
                        .Where(hd => hd.H_ID == deleteDto.hosId && hd.D_ID == deleteDto.branchId)
                        .ExecuteCommandAsync();
                }

                DbContext.db.Ado.CommitTran();
                return Ok("1");
            }
            catch (Exception ex)
            {
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateHosAndDepInfo")]
        public async Task<IActionResult> CreateHosAndDepInfoAsync([FromBody] CreateHADInfoReceptionDto createHADInfoReceptionDto)
        {
            _logger.LogInformation("Received CreateHosAndDepInfo request!");

            try
            {
                DbContext.db.Ado.BeginTran();

                // 获取前端传来的 hos_id 和部门名称
                string hosId = createHADInfoReceptionDto.hosId;
                string branchName = createHADInfoReceptionDto.branchName;

                // 检查 hos_id 是否存在于 hospital 表中
                bool hosExists = await DbContext.db.Queryable<HOSPITAL>()
                                                   .Where(h => h.HOS_ID == hosId)
                                                   .AnyAsync();

                if (!hosExists)
                {
                    // 如果 hos_id 不存在，返回错误响应
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest(new { message = "传入的 hos_id 不存在于医院列表中。" });
                }

                // 判断是否存在同名的部门
                var existingDepartment = await DbContext.db.Queryable<DEPARTMENT>()
                    .FirstAsync(dep => dep.NAME == branchName);

                if (existingDepartment != null)
                {
                    // 检查是否已存在对应hos_id的记录
                    var existingHosDep = await DbContext.db.Queryable<HOS_DEP>()
                                                           .FirstAsync(hd => hd.D_ID == existingDepartment.DEP_ID && hd.H_ID == hosId);
                    if (existingHosDep != null)
                    {
                        DbContext.db.Ado.RollbackTran();
                        return BadRequest("该医院已经有这个部门了");
                    }
                }

                string newDepId;
                if (existingDepartment == null)
                {
                    // 如果不存在同名部门，创建新部门
                    var maxDepId = await DbContext.db.Queryable<DEPARTMENT>()
                        .MaxAsync(dep => dep.DEP_ID);
                    int newDepIdInt = int.Parse(maxDepId) + 1;
                    newDepId = newDepIdInt.ToString().PadLeft(2, '0'); // 添加0前缀;

                    // 添加新部门
                    await DbContext.db.Insertable(new DEPARTMENT
                    {
                        DEP_ID = newDepId,
                        NAME = branchName
                    }).ExecuteCommandAsync();
                }
                else
                {
                    // 如果存在同名部门，直接使用其 ID
                    newDepId = existingDepartment.DEP_ID;
                }

                // 将新部门与医院关联
                await DbContext.db.Insertable(new HOS_DEP
                {
                    H_ID = hosId,
                    D_ID = newDepId
                }).ExecuteCommandAsync();

                DbContext.db.Ado.CommitTran();
                return Ok("1");
            }
            catch (Exception ex)
            {
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("UpdateHosAndDepInfo")]
        public async Task<IActionResult> UpdateHosAndDepInfoAsync([FromBody] UpdateHADInfoReceptionDto updateHADInfoReceptionDto)
        {
            _logger.LogInformation("Received UpdateHosAndDepInfo request!");

            try
            {
                DbContext.db.Ado.BeginTran();

                // 获取前端传来的 hos_id、dep_id 和新部门名称
                string hosId = updateHADInfoReceptionDto.hosId;
                string depId = updateHADInfoReceptionDto.branchId;
                string newBranchName = updateHADInfoReceptionDto.branchName;

                // 判断是否存在这个医院与部门的对应关系
                var hosDepExists = await DbContext.db.Queryable<HOS_DEP>()
                    .AnyAsync(hd => hd.H_ID == hosId && hd.D_ID == depId);

                if (hosDepExists)
                {
                    // 更新部门名称
                    await DbContext.db.Updateable<DEPARTMENT>()
                        .SetColumns(dep => dep.NAME == newBranchName)
                        .Where(dep => dep.DEP_ID == depId)
                        .ExecuteCommandAsync();

                    DbContext.db.Ado.CommitTran();
                    return Ok("1");
                }
                else
                {
                    DbContext.db.Ado.RollbackTran();
                    return NotFound("该医院和部门的对应关系不存在。");
                }
                
            }
            catch (Exception ex)
            {
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, ex.Message);
            }
        }
    }
}
