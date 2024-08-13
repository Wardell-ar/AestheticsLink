using DocumentFormat.OpenXml.InkML;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class OPERATE_TIME
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string OP_TIME_ID { get; set; }
        public DateTime DAY { get; set; }
        public Timestamp START_TIME { get; set; }
        public Timestamp END_TIME { get; set; }
        public string STATUS { get; set; }
        public string ROOM_ID { get; set; }
    }
}
