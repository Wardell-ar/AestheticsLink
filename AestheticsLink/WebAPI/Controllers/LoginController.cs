using Microsoft.AspNetCore.Mvc;
using LogRegService.Dto;
using LogRegService;
using WebAPI.JWTService;
using WebModel.Entity;
using NetTaste;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IServerService _serverService;
        private IJWTService _jwtService;
        private readonly ILogger<LoginController> _logger;
        public LoginController(ICustomerService customerService, IServerService serverService, IJWTService jwtService, ILogger<LoginController> logger)
        {
            _customerService = customerService;
            _serverService = serverService;
            _jwtService = jwtService;
            _logger = logger;
        }

        //登录请求
        [HttpPost("CheckLogin")]
        public async Task<IActionResult> CheckLogin([FromBody] LoginDto login)
        {
            _logger.LogInformation("Received login request: UID={UID}", login.UID);

            if(login != null) 
            {
                if(login.UID == "admin" && login.PSW == "8888")
                {
                    var token = _jwtService.GenerateToken(login);

                    var response = new LoginResponse
                    {
                        role = 0,//登录账号类型，0为管理员
                        token = token
                    };
                    // Return token as response
                    return Ok(response);
                }
            }
            // Check login credentials asynchronously
            var customer = await _customerService.CheckLogin(login);

            if (customer != null)
            {
                // Generate JWT token
                var token = _jwtService.GenerateToken(customer);

                var response = new LoginResponse
                {
                    role = 1,//登录账号类型，1为客户
                    token = token
                };
                // Return token as response
                return Ok(response);
            }
            else
            {
                var server = await _serverService.CheckLogin(login);
                if (server != null)
                {
                    // Generate JWT token
                    var token = _jwtService.GenerateToken(server);

                    var response = new LoginResponse
                    {
                        role = 2,//登录账号类型，2为员工
                        token = token
                    };
                    // Return token as response
                    return Ok(response);
                }
                else
                {
                    var response = new LoginResponse
                    {
                        role = -1,//登录账号类型，2为员工
                        token = null
                    };
                    return Ok(response);
                }
            }
        }
        [Authorize]
        [HttpGet]
        public bool Get()
        {
            //if (_jwtService.ValidateToken(token) == true)
            //    return true;
            //else
            //    return false;
            return true;
        }
    }
    public class LoginResponse
    {
        public int role { get; set; }
        public string token { get; set; }
    }
}