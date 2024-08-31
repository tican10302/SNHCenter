using DTO.Base;
using DTO.System.Menu.Dtos;
using DTO.System.Menu.Models;

namespace REPOSITORY.System.Menu;

public interface IMenuRepository
{
    Task<BaseResponse<List<MenuModel>>> GetList(GetAllRequest request);
    Task<BaseResponse<List<MenuModel>>> GetAll(GetAllRequest request);
    Task<BaseResponse<MenuModel>> GetById(GetByIdRequest request);
    Task<BaseResponse<MenuDto>> GetByPost(GetByIdRequest request);
    Task<BaseResponse<MenuModel>> Insert(MenuDto request);
    Task<BaseResponse<MenuModel>> Update(MenuDto request);
}