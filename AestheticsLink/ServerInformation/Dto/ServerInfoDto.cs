using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ServerInformation.Dto
{
    public class OrderData
    {
        public string order_id { get; set; }
        public string order_name { get; set; }
        public string order_year { get; set; }
        public string order_month { get; set; }
        public string order_day { get; set; }
    }

    public class ServerInfoDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string title { get; set; }
        public string hospital { get; set; }
        public string attendanceStatus { get; set; }
        public List<OrderData> orderData { get; set; }
    }

    public class ServerInfoRequest
    {
        public string id { get; set; }
    }
}
