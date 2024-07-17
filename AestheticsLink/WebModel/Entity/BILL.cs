using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class BILL
    {
        public string BILL_ID { get; set; }
        public string CUS_ID { get; set; }
        public string COU_ID { get; set; }
        public string FOUND_DATE { get; set; }
        public int PAID_AMOUNT { get; set; }
        public string PAY_STATE { get; set; }
        public string HOS_ID { get; set; }
    }
}
