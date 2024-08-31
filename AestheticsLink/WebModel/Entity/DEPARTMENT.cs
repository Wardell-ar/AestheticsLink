using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class DEPARTMENT
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string DEP_ID { get; set; }
        public string NAME { get; set; }
    }
}
