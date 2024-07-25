using Dm.filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSigninService.Signin.Dto;
using ServerSigninService.Signin;
using DocumentFormat.OpenXml.Spreadsheet;
using WebCommon.Database;
using WebModel.Entity;
using SqlSugar;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class SigninController : ControllerBase
    {
        public ISigninService _signinService;
        private readonly ILogger<LoginController> _logger;
        public  SigninController(ISigninService signinService, ILogger<LoginController> logger)
        {
            _signinService = signinService;
            _logger = logger;
        }


        [HttpPost("SigninState")]
        public async Task<string> SigninState([FromBody] SigninDto signin)
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();
                SigninDto signinDto = new SigninDto();
                signinDto.id = signin.id;
                var server = _signinService.CheckSignin(signinDto).Result;
                if (server != null)
                {
                    _logger.LogInformation("Received SigninState request:SER_ID={SER_ID}!", signin.id);

                    if (DbContext.db.Queryable<SERVER>().Any(s => s.SER_ID == signin.id && s.IS_WORK_TODAY == 1))
                    {
                        //事物提交
                        DbContext.db.Ado.CommitTran();
                        return "1";//已签到
                    }
                    else
                    {
                        //事物提交
                        DbContext.db.Ado.CommitTran();
                        return "0";//未签到
                    }
                }
                else
                {
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return "2";//查无此人
                }
            }
            catch (Exception ex)
            {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return "2";//查询出错
            }
        }

        [HttpPost("Signin")]
        public async Task<string> Signin([FromBody] SigninDto signin)
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();
                SigninDto signinDto = new SigninDto();
                signinDto.id = signin.id;
                var server = _signinService.CheckSignin(signinDto).Result;
                if (server != null)
                {
                    _logger.LogInformation("Received Signin request:SER_ID={SER_ID}!", signin.id);

                    if (DbContext.db.Queryable<SERVER>().Any(s => s.SER_ID == signin.id && s.IS_WORK_TODAY == 1))
                    {
                        //事物提交
                        DbContext.db.Ado.CommitTran();
                        return "1";// 已签到
                    }
                    else
                    {
                        var serverSignginToUpdate = DbContext.db.Queryable<SERVER>().Where(c => c.SER_ID == signin.id).First();
                        serverSignginToUpdate.IS_WORK_TODAY = 1;  //把出勤改成1
                        DbContext.db.Updateable(serverSignginToUpdate).ExecuteCommand();
                        //事物提交
                        DbContext.db.Ado.CommitTran();
                        return "1";// 第一次签到
                    }
                }
                else
                {
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return "0";// 查无此人
                }
            }
            catch (Exception ex)
            {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return "0";// 签到失败
            }
                /*SigninDto signinDto = new SigninDto();
            signinDto.id = signin.id;
            var server = _signinService.CheckSignin(signinDto).Result;
            //_logger.LogInformation("Received login request: SER_ID={SER_ID}", signin.SER_ID);
            if (server != null)
            {
                if (DbContext.db.Queryable<SERVER>().Any(s => s.SER_ID == signin.id && s.IS_WORK_TODAY == 1))
                {
                    return "1";// 已签到
                }
                else
                {
                    var serverSignginToUpdate = DbContext.db.Queryable<SERVER>().Where(c => c.SER_ID == signin.id).First();
                    serverSignginToUpdate.IS_WORK_TODAY = 1;  //把出勤改成1
                    DbContext.db.Updateable(serverSignginToUpdate).ExecuteCommand();

                    return "1";// 第一次签到
                }
            }
            else
            {
                return "0";// 签到失败/查无此人
            }*/

        }
    }
}
