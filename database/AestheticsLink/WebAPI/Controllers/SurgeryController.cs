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

        [HttpPost("GetOperationDetails")]
        public async Task<IActionResult> GetOperationDetails([FromBody] SurgeryProjectRequest request)
        {
            try
            {
                var result = await _operationService.GetSurgeryProjectDetailsAsync(request.cusId);
                if (result == null || !result.Any())
                {
                    return Ok("0"); // 修改为返回 "0"
                }
                return Ok(result); // 返回 JSON 数据
            }
            catch (Exception ex)
            {
                // 捕获并记录异常
                Console.WriteLine($"Error in GetOperationDetails: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("GetProjectInfo")]
        public async Task<IActionResult> GetProjectInfo([FromBody] ProjectInfoRequest request)
        {
            try
            {
                var result = await _operationService.GetProjectInfoAsync(request);
                if (result == null || !result.Any())
                {
                    return Ok("0"); // 修改为返回 "0"
                }
                return Ok(result); // 返回 JSON 数据
            }
            catch (Exception ex)
            {
                // 捕获并记录异常
                Console.WriteLine($"Error in GetProjectInfo: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

    // DTO 类定义
    public class SurgeryProjectRequest
    {
        public string cusId { get; set; }
    }
}