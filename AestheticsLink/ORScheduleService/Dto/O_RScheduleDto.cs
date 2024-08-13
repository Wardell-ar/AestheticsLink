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
        public string roomId { get; set; }//手术室id       

        public List<O_RScheduleSlotsDto> slots { get; set; } // 排班的时间段信息
    }

    public class O_RScheduleSlotsDto
    {
        public string status { get; set; } //字符串的0、1；0表示没预约，1表示有预约
        public string info { get; set; } // 预约的手术名称(没有预约时给“无预约”)
        public string doctor { get; set; } //负责手术的医生名字
        public string customer { get; set; } // 进行手术的客户的名字
    }
}
