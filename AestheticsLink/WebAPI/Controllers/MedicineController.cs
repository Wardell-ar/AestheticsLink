using DocumentFormat.OpenXml.Drawing.Spreadsheet;
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

        [HttpPost("CheckStorage")]
        public async Task<IActionResult> CheckStorage([FromBody] CheckStorageDto dto)
        {
            var storage = await _medicineService.CheckStorageAndReplenish(dto.HOS_ID, dto.G_ID);
            var result = new StorageResultDto
            {
                Storage = storage
            };
            return Ok(result);
        }
    }
}
