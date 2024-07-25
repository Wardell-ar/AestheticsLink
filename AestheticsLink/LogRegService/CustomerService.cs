using LogRegService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;
using WebCommon.Database;
using Dm.filter;

namespace LogRegService
{
    public class CustomerService : ICustomerService
    {
        async public Task<CUSTOMER> CheckLogin(LoginDto login)
        {
            try
            {
                // 查询数据库，验证用户名和密码
                var customer = await DbContext.db.Queryable<CUSTOMER>()
                                        .Where(c => c.PHONE_NUM == login.UID && c.PASSWORD == login.PSW)
                                        .FirstAsync();
                return customer;
            }
            catch (Exception ex)
            {
                // 处理异常（比如数据库连接失败等）
                throw new Exception("登录验证失败：" + ex.Message);
            }
        }
        public CustomerDto AddCustomer(RegisterDto register)
        {
            CUSTOMER customer = TransRegisterDto(register);
            try
            {
                if (DbContext.db.Queryable<CUSTOMER>().Any(c => c.PHONE_NUM.Equals(register.UID)))
                {
                    throw new Exception("手机号已存在");
                }
                else
                {
                    DbContext.db.Insertable(customer).ExecuteCommand();
                    return new CustomerDto
                    {
                        CUS_ID = customer.CUS_ID,
                        PHONE_NUM = customer.PHONE_NUM,
                        FOUND_DATE = customer.FOUND_DATE,
                        BIRTHDAY = customer.BIRTHDAY,
                        GENDER = customer.GENDER,
                        NAME = customer.NAME,
                        EX = customer.EX,
                        PASSWORD = customer.PASSWORD,
                        BALANCE = customer.BALANCE,
                        VIPLEVEL = customer.VIPLEVEL,
                    };
                }
            }
            catch (Exception ex)
            {
                // 处理异常的代码，例如记录日志、返回错误信息等
                Console.WriteLine("发生异常：{ex.Message}");
                throw; // 重新抛出异常，或者根据具体情况进行其他处理
            }
        }
        private CUSTOMER TransRegisterDto(RegisterDto register)
        {
            var customer = new CUSTOMER();
            var foundDate = DateTime.Now;
            // 获取数据库中已存在的最大 CUS_ID
            string lastCustomerId = GetMaxCustomerId();
            // 生成新的 CUS_ID
            int newId = int.Parse(lastCustomerId) + 1;

            customer.CUS_ID = newId.ToString();//自己设置
            customer.PHONE_NUM = register.UID;
            customer.FOUND_DATE = foundDate;
            customer.BIRTHDAY = GetDateTime(register.YEAR, register.MONTH, register.DAY);
            customer.GENDER = register.GENDER;
            customer.NAME = register.NAME;
            customer.EX = 0;
            customer.PASSWORD = register.PASSWORD;
            customer.BALANCE = 0;
            customer.VIPLEVEL = "copper";

            return customer;

        }
        private DateTime GetDateTime(string YEAR, string MONTH, string DAY)
        {
            // Convert strings to integers
            int year = int.Parse(YEAR);
            int month = int.Parse(DAY);
            int day = int.Parse(DAY);

            // Create DateTime object
            DateTime dateTime = new DateTime(year, month, day);

            return dateTime;
        }
        private string GetMaxCustomerId()
        {
            string sql = "SELECT MAX(CUS_ID) FROM CUSTOMER"; // 替换YourTableName为实际的表名

            // 执行 SQL 查询
            var result = DbContext.db.Ado.GetString(sql);

            return result;
        }
    }
}
