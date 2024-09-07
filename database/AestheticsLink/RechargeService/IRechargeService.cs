using RechargeService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechargeService
{
    public interface IRechargeService
    {
        public bool CustomerRecharge(RechargeDto recharge);
    }
}
