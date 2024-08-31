using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAllUsersService.QueryAllServers.Dto
{
    public class QueryServersDto
    {
        public string name { get; set; }
        public string ser_id { get; set; }
        public string gender { get; set; }
        public string phone_num { get; set; }
        public DateTime joined_date { get; set; }
        public decimal salary { get; set; }
        public string position { get; set; }
        public string hos_name { get; set; }
        public string dep_name { get; set; }
    }

    public class DeleteServersDto
    {
        public List<string> ser_ids { get; set; }
    }

    public class UpdateServersDto
    {
        public string ser_id { get; set; }
        public decimal salary { get; set; }
        public string position { get; set; }
        public string hos_name { get; set; }
        public string dep_name { get; set; }
    }
}
