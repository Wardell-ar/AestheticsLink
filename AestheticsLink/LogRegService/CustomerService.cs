using LogRegService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;
using WebCommon.Database;
using Dm.filter;
using DocumentFormat.OpenXml.Presentation;

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
                                        .Where(c => c.PHONE_NUM == login.uid && c.PASSWORD == login.psw)
                                        .FirstAsync();

                // 检测当天是否是客户的生日
                if (customer.BIRTHDAY.Date == DateTime.Now.Date)
                {
                    // 生日匹配，返回客户信息并告知今天是他们的生日
                    customer.NAME += "，祝你生日快乐！";
                }

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

            if (DbContext.db.Queryable<CUSTOMER>().Any(c => c.PHONE_NUM.Equals(register.uid)))
            {
                return null;
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
                    PASSWORD = customer.PASSWORD,
                    BALANCE = customer.BALANCE,
                    VIPLEVEL = customer.VIPLEVEL,
                };
            }


        }

        private CUSTOMER TransRegisterDto(RegisterDto register)
        {
            var customer = new CUSTOMER();
            var foundDate = DateTime.Now;
            // 获取数据库中已存在的最大 CUS_ID
            string lastCustomerId = GetMaxCustomerId();
            // 生成新的 CUS_ID
            int newId;
            if (lastCustomerId == "")
            {
                newId = 1;
            }
            else
            {
                newId = int.Parse(lastCustomerId) + 1;
            }
            customer.CUS_ID = newId.ToString();//自己设置
            customer.PHONE_NUM = register.uid;
            customer.FOUND_DATE = foundDate;
            customer.BIRTHDAY = GetDateTime(register.year, register.month, register.day);
            customer.GENDER = register.gender;
            customer.NAME = register.name;
            customer.PASSWORD = register.psw;
            customer.BALANCE = 0;
            customer.VIPLEVEL = "Copper";

            return customer;

        }
        private DateTime GetDateTime(string YEAR, string MONTH, string DAY)
        {
            // Convert strings to integers
            int year = int.Parse(YEAR);
            int month = int.Parse(MONTH);
            int day = int.Parse(DAY);

            // Create DateTime object
            DateTime dateTime = new DateTime(year, month, day);

            return dateTime;
        }
        private string GetMaxCustomerId()
        {
            string sql = "SELECT MAX(TO_NUMBER(CUS_ID)) FROM CUSTOMER";

            // 执行 SQL 查询
            var result = DbContext.db.Ado.GetString(sql);

            return result;
        }
    }
}
