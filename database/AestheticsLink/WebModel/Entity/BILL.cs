using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class BILL
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string BILL_ID { get; set; }
        public string CUS_ID { get; set; }
        public string COU_ID { get; set; }
        public DateTime FOUND_DATE { get; set; }
        public decimal PAID_AMOUNT { get; set; }
        public string HOS_ID { get; set; }
    }
}
