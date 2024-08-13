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
        public string ROOM_ID { get; set; }//手术室id
        public string HOS_ID { get; set; }//医院id
        public string HOS_NAME { get; set; }//医院名称

        public string PROJ_ID { get; set; }//项目id
        public string NAME { get; set; }//项目名称
        public string PRICE { get; set; }//手术价格
        public DateTime DAY { get; set; }//手术日期
        public string STATUS { get; set; }//手术状态
        public string OP_TIME_ID { get; set; }//手术时间id
        public Timestamp START_TIME { get; set; }//手术开始时间
        public Timestamp END_TIME { get; set; }//手术结束时间

        public string SER_ID { get; set; }//员工id
        public string SER_NAME { get; set; }//员工姓名

        public string CUS_ID { get; set; }//顾客id
        public string CUS_NAME { get; set; }//顾客姓名
        public string BILL_ID { get; set; }//账单id
    }
}
