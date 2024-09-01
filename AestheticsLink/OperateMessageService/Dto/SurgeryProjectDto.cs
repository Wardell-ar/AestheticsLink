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
        public string hospital { get; set; }
        public string name { get; set; }
        //public string ServerName { get; set; }
        //public string GoodsName { get; set; }
        //public decimal GoodsPrice { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string startTime { get; set; }  // 字符串类型
        public string endTime { get; set; }    // 字符串类型
        public string duration { get; set; }   // 手术持续时间
        public string room { get; set; }
        public string status { get; set; }

        public string billid { get; set; }
    }

    public class ProjectDto
    {
        public string proj_id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string found_date { get; set; }
    }

    public class ProjectInfoRequest
    {
        public string proj_id { get; set; }
        public string name { get; set; }
    }

}
