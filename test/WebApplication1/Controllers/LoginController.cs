using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebService.User;
using WebService.User.Dto;
using WenCommon;

namespace WebApplication1.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IUserService _userService;
        //private ICustomJWTService _iJWTService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        //验证码
        [HttpGet]
        public IActionResult GetValidateCodeImage(string t)
        {
            System.Console.WriteLine(t);
            var validateCodeString = Tools.CreateValidateString();
            //将验证码计入缓存
            MemoryHelper.SetMemory(t, validateCodeString, 1);

            //接收图片返回的二进制流
            byte[] buffer = Tools.CreateValidateCodeBuffer(validateCodeString);
            return File(buffer, @"image/jpeg");
        }
        ////登录
        //[HttpGet("{Cus_Id}/{Password}/{validateKey}/{validateCode}")]
        //public string CheckLogin(string Cus_Id, string Password, string validateKey, string validateCode)
        //{
        //    var currCode = MemoryHelper.GetMemory(validateKey);
        //    if (currCode == null)
        //    {
        //        return "";
        //    }
        //    if (currCode.ToString() == validateCode)
        //    {
        //        LoginDto loginDto = new LoginDto();
        //        loginDto.Cus_Id = Cus_Id;
        //        loginDto.Password = Password;
        //        var user = _userService.CheckLogin(loginDto).Result;
        //        if (user != null)
        //        {
        //            return _iJWTServices.GetTokens(User);
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    else
        //        return "";
        //}
    }
}
