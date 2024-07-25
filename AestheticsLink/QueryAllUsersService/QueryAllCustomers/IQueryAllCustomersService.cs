using QueryAllUsersService.QueryAllCustomers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;

namespace QueryAllUsersService.QueryAllCustomers
{
    public interface IQueryAllCustomersService
    {
        Task<List<QueryCustomersDto>> GetCustomersByCriteria(Dictionary<string, object> criteria);

        int CalculateAge(DateTime birthday);

        int GetIntFromJsonElement(object obj);
    }
}
