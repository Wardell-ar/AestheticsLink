using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.User.Dto
{
    public class InputUserDto
    {
        public string CUS_ID { get; set; }
        public string PHONE_NUM { get; set; }
        public DateTime BIRTHDAY { get; set; }
        public string GENDER { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public string validateKey { get; set; }
        public string validateCode { get; set; }

    }
}
