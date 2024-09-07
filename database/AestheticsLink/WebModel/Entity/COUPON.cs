using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class COUPON
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string COU_ID { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }
        public decimal PRICE { get; set; }
    }
}
