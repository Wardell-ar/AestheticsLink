using LogRegService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;
using WebCommon.Database;

namespace LogRegService
{
    public class CustomerService : ICustomerService
    {
        public CustomerDto AddCustomer(RegisterDto input)
        {
            throw new NotImplementedException();
        }

        async public Task<CUSTOMER> CheckLogin(LoginDto login)
        {
            try
            {
                // 查询数据库，验证用户名和密码
                var customer = await DbContext.db.Queryable<CUSTOMER>()
                                        .Where(c => c.CUS_ID == login.UID && c.PASSWORD == login.PSW)
                                        .FirstAsync();

                return customer;
            }
            catch (Exception ex)
            {
                // 处理异常（比如数据库连接失败等）
                throw new Exception("登录验证失败：" + ex.Message);
            }
        }

    }
}
