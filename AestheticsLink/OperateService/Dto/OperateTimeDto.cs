using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.Dto
{
    public class OperateTimeDto
    {
        public string OP_TIME_ID { get; set; }
        public string ROOM_ID { get; set; }
        public DateOnly DAY { get; set; }
        public DateTime START_TIME { get; set; }
        public DateTime END_TIME { get; set; }
        public string STATUS {  get; set; }

    }
}
