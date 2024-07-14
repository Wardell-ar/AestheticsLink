using LogRegService.Dto;
using WebModel.Entity;

namespace WebAPI.JWTService
{
    public interface IJWTService
    {
        string GenerateToken(CUSTOMER customer);
        string GenerateToken(SERVER server);
        string GenerateToken(LoginDto admin);
        public bool ValidateToken(string token);
    }
}
