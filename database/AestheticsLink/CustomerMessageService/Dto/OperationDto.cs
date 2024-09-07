using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMessageService.Dto
{
    public class OperationDto
    {
        public string ServerName { get; set; }
        public string ProjectName { get; set; }
        public string Price { get; set; }
        public DateTime FoundDate { get; set; }
    }
}
