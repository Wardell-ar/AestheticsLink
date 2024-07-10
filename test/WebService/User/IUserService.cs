using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;
using WebService.User.Dto;

namespace WebService.User
{
    //接口
    public interface IUserService
    {
        //登录
        Task<CUSTOMER> CheckLogin(LoginDto login);


        //注册
        UserDto AddUser(InputUserDto input);
    }
}
