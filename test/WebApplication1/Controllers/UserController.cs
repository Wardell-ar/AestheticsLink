using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebService.User;
using WebService.User.Dto;

namespace WebApplication1.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("这是一个Get请求！");
            return "这是一个Get请求！";
        }
        //用户注册
        [HttpPost]
        public ActionResult<UserDto> RegistUser([FromBody] InputUserDto input)
        {
            try
            {
                var res = _userService.AddUser(input);
                return res;
            }
            catch(System.Exception ex)
            {
                JsonResult result = new JsonResult(ex)
                {
                    StatusCode = 201,
                };
                return result;
            }
        }
    }
}
