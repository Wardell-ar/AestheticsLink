﻿using System;
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
        public string PhoneNum { get; set; }
        public DateTime FoundDate { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public decimal Balance { get; set; }
        public string VIPLevel { get; set; }
        public List<CouponDto> Coupons { get; set; }
        public List<OperationDto> Operations { get; set; }

    }
}