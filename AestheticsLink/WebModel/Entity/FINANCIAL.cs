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
        public string HOS_ID { get; set; }
        public DateTime FINANCE_MONTH { get; set; }
        public decimal INCOME { get; set; }
        public decimal PAYOUT { get; set; }
    }
}