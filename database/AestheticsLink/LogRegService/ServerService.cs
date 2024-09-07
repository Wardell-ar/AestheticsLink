using Dm.filter;
using LogRegService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace LogRegService
{
    public class ServerService : IServerService
    {
        public async Task<SERVER> CheckLogin(LoginDto login)
        {
            try
            {
                // 查询数据库，验证用户名和密码
                var server = await DbContext.db.Queryable<SERVER>()
                                        .Where(c => c.PHONE_NUM == login.uid && c.PASSWORD == login.psw)
                                        .FirstAsync();

                return server;
            }
            catch (Exception ex)
            {
                // 处理异常（比如数据库连接失败等）
                throw new Exception("登录验证失败：" + ex.Message);
            }

        }
    }
}
