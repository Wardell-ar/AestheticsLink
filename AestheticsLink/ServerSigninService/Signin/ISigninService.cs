using Dm.filter;
using ServerSigninService.Signin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon;
using WebModel.Entity;

namespace ServerSigninService.Signin
{
    public interface ISigninService
    {
        Task<SERVER> CheckSignin(SigninDto signin);
    }
}
