using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class OPERATING_ROOM
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ROOM_ID { get; set; }
        public string HOS_ID { get; set; }
    }
}
