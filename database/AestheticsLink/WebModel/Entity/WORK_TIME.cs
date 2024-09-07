using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class WORK_TIME
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string WORK_TIME_ID { get; set; }
        public int DAY1 { get; set; }
        public int DAY2 { get; set; }
        public int DAY3 { get; set; }
        public int DAY4 { get; set; }
        public int DAY5 { get; set; }
        public int DAY6 { get; set; }
        public int DAY7 { get; set; }
    }
}
