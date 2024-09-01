using FinancialService.Dto;
using LogRegService;
using Microsoft.AspNetCore.Mvc;
using ProjectChange;
using ProjectChange.Dto;
using WebCommon.Database;
using WebModel.Entity;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]

    public class ProjectChangeController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectChangeController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpPost("AddProject")]
        public IActionResult AddProject([FromBody] NewProject project)
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();
                _projectService.AddProject(project);
                DbContext.db.Ado.CommitTran();
                return Ok("1");

            }
            catch
            {
                DbContext.db.Ado.RollbackTran();
                return Ok("0");
            }
        }
        [HttpPost("RemoveProject")]
        public IActionResult RemoveProject([FromBody] RemoveProjectDto project)
        {
            try
            {
                DbContext.db.Ado.BeginTran();
                _projectService.RemoveProject(project);
                DbContext.db.Ado.CommitTran();
                return Ok("1");

            }
            catch
            {
                DbContext.db.Ado.RollbackTran();
                return Ok("0");
            }
        }
        [HttpPost("ChangeProject")]
        public IActionResult ChangeProject([FromBody] ChangePriceDto project)
        {
            try
            {
                DbContext.db.Ado.BeginTran();
                _projectService.ChangePrice(project);
                DbContext.db.Ado.CommitTran();
                return Ok("1");

            }
            catch
            {
                DbContext.db.Ado.RollbackTran();
                return Ok("0");
            }
        }


    }
}
