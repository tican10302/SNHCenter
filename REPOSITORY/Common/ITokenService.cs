using DAL.Entities;
using DTO.System.Account.Dtos;
using Microsoft.Extensions.Configuration;

namespace REPOSITORY.Common;

public interface ITokenService
{
    string CreateToken(Account acc, IConfiguration config);
}