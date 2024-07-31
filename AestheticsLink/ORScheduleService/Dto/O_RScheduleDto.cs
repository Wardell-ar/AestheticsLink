using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORScheduleService.Dto
{
    public class QueryO_RScheduleDto
    {
        public string ROOM_ID { get; set; }
        public string HOS_ID { get; set; }
        public string HOS_NAME { get; set; }

        public string PROJ_ID { get; set; }
        public string NAME { get; set; }
        public string PRICE { get; set; }
        public DateTime DAY { get; set; }
        public string EXE_STATE { get; set; }
        public string OP_TIME_ID { get; set; }
        public Timestamp START_TIME { get; set; }
        public Timestamp END_TIME { get; set; }

        public string SER_ID { get; set; }
        public string SER_NAME { get; set; }

        public string CUS_ID { get; set; }
        public string CUS_NAME { get; set; }
        public string BILL_ID { get; set; }
    }
}
