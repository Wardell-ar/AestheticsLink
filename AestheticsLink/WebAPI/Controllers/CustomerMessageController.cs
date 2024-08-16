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

        [HttpGet("{cusId}")]
        public async Task<IActionResult> GetCustomerInfo(string cusId)
        {
            var customerInfo = await _customerMessageService.GetCustomerInfoAsync(cusId);
            if (customerInfo == null)
            {
                return NotFound();
            }
            return Ok(customerInfo);
        }
    }
}

