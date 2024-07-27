using DAL.Entities;
using DTO.System.Account.Dtos;

namespace REPOSITORY.Common;

public interface ITokenService
{
    string CreateToken(Account acc);
}