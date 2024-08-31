using CustomerMessageService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class CustomerMessageController : ControllerBase
    {
        private readonly ICustomerMessageService _customerMessageService;

        public CustomerMessageController(ICustomerMessageService customerMessageService)
        {
            _customerMessageService = customerMessageService;
        }

        // 使用 [HttpPost] 接收 JSON 数据
        [HttpPost]
        public async Task<IActionResult> GetCustomerInfo([FromBody] CustomerInfoRequest request)
        {
            try
            {
                var customerInfo = await _customerMessageService.GetCustomerInfoAsync(request.CusId);
                if (customerInfo == null)
                {
                    return NotFound();
                }
                return Ok(customerInfo); // 返回 JSON 格式的数据
            }
            catch (Exception ex)
            {
                // 捕获并记录异常
                Console.WriteLine($"Error in GetCustomerInfo: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("updatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            try
            {
                var result = await _customerMessageService.UpdateCustomerPasswordAsync(request.cusId, request.newpassword);

                if (result == 1)
                {
                    return Ok("1"); // 修改成功
                }
                else
                {
                    return Ok("0"); // 修改失败
                }
            }
            catch (Exception ex)
            {
                // 捕获并记录异常
                Console.WriteLine($"Error in UpdatePassword: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

    // DTO 类定义
    public class CustomerInfoRequest
    {
        public string CusId { get; set; }
    }

    public class UpdatePasswordRequest
    {
        public string cusId { get; set; }
        public string newpassword { get; set; }
    }
}