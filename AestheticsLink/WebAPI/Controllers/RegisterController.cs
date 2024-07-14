using LogRegService;
using LogRegService.Dto;
using Microsoft.AspNetCore.Mvc;
using WebModel.Entity;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ICustomerService _customerService;
        public RegisterController(ILogger<LoginController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }
        [HttpPost("Register")]
        public async Task<string> Register([FromBody] RegisterDto register)
        {
            _logger.LogInformation("Received register request: UID={UID}", register.UID);
            //检查电话号码有无被注册过
            var customer =  _customerService.AddCustomer(register);
            if (customer == null) 
            {
                //注册失败，电话号码已经被注册
                return "0";
            }
            else
            {
                //注册成功
                return "1";
            }
        }


    }

}
