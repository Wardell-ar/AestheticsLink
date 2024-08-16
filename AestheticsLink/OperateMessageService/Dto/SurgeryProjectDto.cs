using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using DocumentFormat.OpenXml.InkML;

namespace SurgeryProjectService.Dto
{
    public class SurgeryProjectDto
    {
        public string ProjectName { get; set; }
        public string ServerName { get; set; }
        public string GoodsName { get; set; }
        public decimal GoodsPrice { get; set; }
        public DateTime FoundDate { get; set; }
        public DateTime OperationDay { get; set; }
        public Timestamp StartTime { get; set; }
        public Timestamp EndTime { get; set; }
        public string RoomId { get; set; }
    }
}
