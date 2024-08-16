using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class HOSPITALBILL
    {
        public DateTime FOUND_DATE { get; set; }
        public decimal OUTCOME { get; set; }
        public decimal INCOME { get; set; }
        public string HOS_ID { get; set; }
    }
}
