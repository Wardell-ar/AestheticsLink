using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAllUsersService.QueryAllCustomers.Dto
{
    public class QueryCustomersDto
    {
        public string name { get; set; }
        public string cus_id { get; set; }
        public string gender { get; set; }
        public string phone_num { get; set; }
        public int age { get; set; }
        public DateTime birthday { get; set; }
        public string viplevel { get; set; }
        public decimal balance { get; set; }
    }

    public class DeleteCustomersDto
    {
        public List<string> cus_ids { get; set; }
    }
}
