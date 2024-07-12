using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class CUSTOMER
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string CUS_ID { get; set; }
        public string PHONE_NUM { get; set; }
        public DateTime FOUND_DATE { get; set; }
        public DateTime BIRTHDAY { get; set; }
        public string GENDER { get; set; }
        public string NAME { get; set; }
        public int EX { get; set; }
        public string PASSWORD { get; set; }
        public int BALANCE { get; set; }
        public string VIPLEVEL { get; set; }
    }
}
