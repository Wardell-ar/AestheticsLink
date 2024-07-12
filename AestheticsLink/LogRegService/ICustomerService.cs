using LogRegService.Dto;
using WebModel.Entity;

namespace LogRegService
{
    public interface ICustomerService
    {
        //登录服务
        Task<CUSTOMER> CheckLogin(LoginDto login);

        //注册服务
        CustomerDto AddCustomer(RegisterDto input);
    }
}
