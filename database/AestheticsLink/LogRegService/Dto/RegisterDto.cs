using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogRegService.Dto
{
    public class RegisterDto
    {
        public string name { get; set; }
        public string gender { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string uid { get; set; }
        public string psw { get; set; }
    }
}
