using Microsoft.AspNetCore.Mvc;
using LogRegService.Dto;
using LogRegService;
//using WebAPI.JWTService;
using WebModel.Entity;
using NetTaste;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.Spreadsheet;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class LoginController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IServerService _serverService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ICustomerService customerService, IServerService serverService, ILogger<LoginController> logger)
        {
            _customerService = customerService;
            _serverService = serverService;
            _logger = logger;
        }

        //登录请求
        [HttpPost("CheckLogin")]
        public async Task<IActionResult> CheckLogin([FromBody] LoginDto login)
        {
            _logger.LogInformation("Received login request: UID={UID}", login.uid);

            if (login != null)
            {
                if (login.uid == "admin" && login.psw == "8888")
                {

                    var response = new LoginResponse
                    {
                        id = "admin",
                        role = "0"//登录账号类型，0为管理员
                    };
                    
                    return Ok(response);
                }
            }
            // Check login credentials asynchronously
            var customer = await _customerService.CheckLogin(login);

            if (customer != null)
            {
                var response = new LoginResponse
                {
                    id = customer.CUS_ID,
                    role = "1"//登录账号类型，1为客户
                };
                // Return token as response
                return Ok(response);
            }
            else
            {
                var server = await _serverService.CheckLogin(login);
                if (server != null)
                {

                    var response = new LoginResponse
                    {
                        id = server.SER_ID,
                        role = "2"//登录账号类型，2为员工
                    };

                    return Ok(response);
                }
                else
                {
                    var response = new LoginResponse
                    {
                        id = "-1",
                        role = "3"//登录账号类型，2为员工
                    };
                    return Ok(response);
                }
            }
        }

    }
    public class LoginResponse
    {
        public string role { get; set; }
        public string id { get; set; }
    }
}