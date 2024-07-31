using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class PROJECT
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string PROJ_ID { get; set; }
        public string NAME { get; set; }
        public string PRICE { get; set; }
        public DateTime FOUND_DATE { get; set; }
    }
}
