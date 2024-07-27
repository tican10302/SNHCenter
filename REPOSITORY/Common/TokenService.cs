using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoDependencyRegistration.Attributes;
using DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace REPOSITORY.Common;

[RegisterClassAsScoped]
public class TokenService(IConfiguration config) : ITokenService
{
    private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty));

    public string CreateToken(Account acc)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, acc.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.NameId, acc.Id.ToString())
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = config["Jwt:Issuer"] ?? string.Empty,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(DTO.Common.CommonConst.ExpireTime),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}