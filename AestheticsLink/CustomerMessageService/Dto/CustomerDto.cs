using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMessageService.Dto
{
    public class CustomerDto
    {
        public string CusId { get; set; }
        public string Name { get; set; }
        //public string PhoneNum { get; set; }
        public string Year { get; set; }  // 年
        public string Month { get; set; } // 月
        public string Day { get; set; }   // 日
        public string Gender { get; set; }

        public decimal Money { get; set; }
        public string VIPLevel { get; set; }
        //public List<CouponDto> Coupons { get; set; }
        //public List<OperationDto> Operations { get; set; }
        public string Password { get; set; }


    }
}
