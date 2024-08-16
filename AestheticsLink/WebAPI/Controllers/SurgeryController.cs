using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeryProjectService;
using SurgeryProjectService.Dto;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurgeryController : ControllerBase
    {
        private readonly ISurgeryProjectService _operationService;

        public SurgeryController(ISurgeryProjectService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet("{projId}")]
        public async Task<IActionResult> GetOperationDetails(string projId)
        {
            var result = await _operationService.GetSurgeryProjectDetailsAsync(projId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
