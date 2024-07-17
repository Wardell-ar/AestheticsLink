using DocumentFormat.OpenXml.Spreadsheet;
using LogRegService.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerInformation.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebModel.Entity;

namespace WebAPI.JWTService
{
    public class JWTService : IJWTService
    {
        private readonly JWTTokenOptions _jwtTokenOptions;
        public JWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            _jwtTokenOptions = jwtTokenOptions.CurrentValue;
            _jwtTokenOptions.Secret = GenerateSecret();
        }
        //生成Secret
        public static string GenerateSecret(int length = 32)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var key = new byte[length];
                rng.GetBytes(key);
                return Convert.ToBase64String(key); // Base64编码以确保它是一个安全的字符串
            }
        }
        //生成Token
        public string GenerateToken(CUSTOMER customer)
        {
            if (_jwtTokenOptions == null)
            {
                throw new NullReferenceException("JWTTokenOptions is not configured properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, customer.CUS_ID.ToString()),
            new Claim("PHONE_NUM", customer.PHONE_NUM),
            new Claim("NAME", customer.NAME)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtTokenOptions.Issuer,
                audience: _jwtTokenOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        string IJWTService.GenerateToken(SERVER server)
        {
            if (_jwtTokenOptions == null)
            {
                throw new NullReferenceException("JWTTokenOptions is not configured properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, server.SER_ID.ToString()),
                new Claim("PHONE_NUM", server.PHONE_NUM),
                new Claim("NAME", server.NAME)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtTokenOptions.Issuer,
                audience: _jwtTokenOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        string IJWTService.GenerateToken(LoginDto admin)
        {
            if (_jwtTokenOptions == null)
            {
                throw new NullReferenceException("JWTTokenOptions is not configured properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, admin.uid.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtTokenOptions.Issuer,
                audience: _jwtTokenOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        string IJWTService.GenerateToken(ServerInfoDto serverinfo)
        {
            if (_jwtTokenOptions == null)
            {
                throw new NullReferenceException("JWTTokenOptions is not configured properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, serverinfo.id.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtTokenOptions.Issuer,
                audience: _jwtTokenOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // 验证 JWT
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtTokenOptions.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenOptions.Secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                return true; // 验证成功
            }
            catch (Exception)
            {
                return false; // 验证失败
            }
        }
    }
}
