using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerMessageService.Dto;

namespace CustomerMessageService
{
    public interface ICustomerMessageService
    {
        Task<CustomerDto> GetCustomerInfoAsync(string cusId);
        Task<int> UpdateCustomerPasswordAsync(string cusId, string newPassword);
        Task<VipDto> GetCustomerVipInfoAsync(string cusId);
        Task<List<CouponDto>> GetCustomerCouponInfoAsync(string cusId);
    }
}
