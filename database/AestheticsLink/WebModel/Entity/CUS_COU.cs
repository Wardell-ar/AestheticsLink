using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class CUS_COU
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string CUS_ID { get; set; }
        public string COU_ID { get; set; }
    }
}
