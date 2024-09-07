using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class HOSPITAL
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string HOS_ID { get; set; }
        public DateTime DATE_OF_ESTABLISHMENT { get; set; }
        public string NAME { get; set; }
        public string QUALIFICATION { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NUMBER {get; set; }
    }
}
