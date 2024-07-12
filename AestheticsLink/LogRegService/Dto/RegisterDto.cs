using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogRegService.Dto
{
    public class RegisterDto
    {
        public string CUS_ID { get; set; }
        public string PHONE_NUM { get; set; }
        public DateTime BIRTHDAY { get; set; }
        public string GENDER { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
    }
}
