using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoDependencyRegistration.Attributes;
using DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace REPOSITORY.Common;

[RegisterClassAsScoped]
public class TokenService : ITokenService
{
    public string CreateToken(Account acc, IConfiguration config)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Name, acc.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, acc.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var token = new JwtSecurityToken(config["Jwt:Issuer"],
            config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(DTO.Common.CommonConst.ExpireTime),
            signingCredentials: credentials);

        return tokenHandler.WriteToken(token);
    }
}