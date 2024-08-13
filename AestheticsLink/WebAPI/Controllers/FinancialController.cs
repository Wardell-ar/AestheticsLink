using DocumentFormat.OpenXml.Bibliography;
using FinancialService.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OrderService;
using OrderService.Dto;
using System.Globalization;
using WebCommon.Database;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]

    public class FinancialController : ControllerBase
    {
        [HttpPost("GetFinancial")]
        public IActionResult GetFinancial([FromBody] FinancialDto financial)
        {
            try
            {
                // 将月份和年份合并成一个字符串
                string monthYear = financial.month + "-" + financial.year;
                // 使用 DateTime.ParseExact 方法进行转换
                DateTime dateTime = DateTime.ParseExact(monthYear, "MM-yyyy", null);
                var result = DbContext.db.Ado.SqlQuerySingle<FinancialReturnDto>(
                "SELECT INCOME, PAYOUT " +
                    "FROM FINANCIAL NATURAL JOIN HOSPITAL " +
                    "WHERE NAME = :name AND TO_CHAR(FINANCE_MONTH, 'YYYY-MM') = TO_CHAR(:date, 'YYYY-MM')",
                    new
                    {
                        name = financial.hospitalname,
                        date = dateTime.Date,
                    });
                if (result == null)
                {
                    return Ok(new FinancialReturnDto { income = -1, payout = -1 });
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("GetHospital")]
        public IActionResult GetHospital()
        {
            var hospitalname = DbContext.db.Ado.SqlQuery<string>(
               "SELECT NAME " +
               "FROM HOSPITAL ");
            return Ok(hospitalname);
        }

    }
}
