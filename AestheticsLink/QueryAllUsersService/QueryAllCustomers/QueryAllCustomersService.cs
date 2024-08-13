using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QueryAllUsersService.QueryAllCustomers.Dto;
using WebCommon.Database;
using WebModel.Entity;

namespace QueryAllUsersService.QueryAllCustomers
{
    public class QueryAllCustomersService : IQueryAllCustomersService
    {

        public int CalculateAge(DateTime birthday)
        {
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;
            return age;
        }

        public int GetIntFromJsonElement(object obj)
        {
            return obj is JsonElement jsonElement ? jsonElement.GetInt32() : Convert.ToInt32(obj);
        }

        public async Task<List<QueryCustomersDto>> GetCustomersByCriteria(Dictionary<string, object> criteria)
        {
            var query = DbContext.db.Queryable<CUSTOMER>();
            foreach (var criterion in criteria)
            {
                switch (criterion.Key.ToLower())
                {
                    case "phone_num":
                        query = query.Where(c => c.PHONE_NUM == criterion.Value.ToString());
                        break;
                    case "gender":
                        query = query.Where(c => c.GENDER == criterion.Value.ToString());
                        break;
                    case "name":
                        query = query.Where(c => c.NAME == criterion.Value.ToString());
                        break;
                    case "viplevel":
                        query = query.Where(c => c.VIPLEVEL == criterion.Value.ToString());
                        break;
                }
            }

            var customers = await query.Select(c => new QueryCustomersDto
            {
                name = c.NAME,
                cus_id = c.CUS_ID,
                gender = c.GENDER,
                birthday = c.BIRTHDAY,
                phone_num = c.PHONE_NUM,
                viplevel = c.VIPLEVEL,
                balance = c.BALANCE
            }).ToListAsync();

            // 在内存中计算年龄或其他复杂操作
            var filteredCustomers = new List<QueryCustomersDto>();
            foreach (var customer in customers)
            {
                customer.age = CalculateAge(customer.birthday);
                var isValid = true;

                if (criteria.TryGetValue("agemin", out var ageMinObj))
                {
                    int ageMin = GetIntFromJsonElement(ageMinObj);
                    if (customer.age < ageMin)
                    {
                        isValid = false;
                    }
                }

                if (criteria.TryGetValue("agemax", out var ageMaxObj))
                {
                    int ageMax = GetIntFromJsonElement(ageMaxObj);
                    if (customer.age > ageMax)
                    {
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    filteredCustomers.Add(customer);
                }
            }

            return filteredCustomers;

        }
    }
}
