using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Dto
{
    public class RestChoiceDto
    {
        public string HOS_ID { get; set; }
        public string COU_ID { get; set; }
        public string BILL_ID { get; set; }
        public string CUS_ID { get; set; }
        public decimal PAID_AMOUNT { get; set; }

    }
}
