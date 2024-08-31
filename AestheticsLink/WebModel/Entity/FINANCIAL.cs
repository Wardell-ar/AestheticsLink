using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class FINANCIAL
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public int HOS_ID { get; set; }
        public string FINANCE_MONTH { get; set; }
        public string INCOME { get; set; }
        public string PAYOUT { get; set; }
    }
}
