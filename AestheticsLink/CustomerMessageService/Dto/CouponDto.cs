using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMessageService.Dto
{
    public class CouponDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Year { get; set; } // 优惠券有效期的年
        public string Month { get; set; } // 优惠券有效期的月
        public string Day { get; set; } // 优惠券有效期的日
    }

    public class VipDto
    {
        public string vipLevel { get; set; }

        public decimal discount { get; set; }
    }

}
