using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class OPERATE
    {
        public string PROJ_ID { get; set; }
        public string SER_ID { get; set; }
        public string BILL_ID { get; set; }
        public DateTime FOUND_DATE { get; set; }
        public string EXE_STATE { get; set; }
        public string OP_TIME_ID { get; set; }
    }
}
