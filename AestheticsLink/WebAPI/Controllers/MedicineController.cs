using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedcineService.Dto;
using MedcineService;
using SqlSugar;
using WebCommon.Database;
using WebModel.Entity;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // 查询库存的接口，接收 HOS_ID 和药品名称 (NAME)
        [HttpPost("CheckStorage")]
        public async Task<IActionResult> CheckStorage([FromBody] CheckStorageDto dto)
        {
            try
            {
                // 记录传入的参数
                Console.WriteLine($"Simulated input - hos_id: {dto.hos_id}, name: {dto.name}");

                // 调用服务获取药品信息
                var medicineInfo = await _medicineService.GetMedicineInfo(dto);
                // 如果查询结果为空，返回 "0"
                if (medicineInfo == null || !medicineInfo.Any())
                {
                    return Ok("0");
                }
                return Ok(medicineInfo);
            }
            catch (Exception ex)
            {
                // 捕获并记录异常
                Console.WriteLine($"Error in CheckStorage: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // 自动补货接口，不需要前端传递数据
        [HttpPost("AutoReplenish")]
        public async Task<IActionResult> AutoReplenish()
        {
            try
            {
                await _medicineService.AutoReplenishStorage();
                return Ok(new { Message = "Replenishment completed." });
            }
            catch (Exception ex)
            {
                // 捕获并记录异常
                Console.WriteLine($"Error in AutoReplenish: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}