using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogRegService.Dto
{
    public class RegisterDto
    {
        public string UID { get; set; }
        public string GENDER { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public string YEAR { get; set; }
        public string MONTH { get; set; }
        public string DAY { get; set; }
    }
}
