using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class SERVER
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string SER_ID { get; set; }
        public DateTime JOINED_DATE { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public string POSITION { get; set; }
        public string PHONE_NUM { get; set;}
        public decimal SALARY { get; set; }
        public string HOS_ID { get; set;}
        public string DEP_ID { get; set; }
        public string WORK_TIME_ID { get; set; }
        public int IS_WORK_TODAY { get; set; }
    }
}
