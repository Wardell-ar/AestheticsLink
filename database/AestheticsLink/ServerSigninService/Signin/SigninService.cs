using Dm.filter;
using ServerSigninService.Signin.Dto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace ServerSigninService.Signin
{
    public class SigninService : ISigninService
    {
        public async Task<SERVER> CheckSignin(SigninDto signin)
        {
            return await DbContext.db.Queryable<SERVER>().FirstAsync(s => s.SER_ID.Equals(signin.id));
        }
    }
}
