using DTO.Base;
using DTO.System.Menu.Dtos;
using DTO.System.Menu.Models;

namespace REPOSITORY.System.Menu;

public interface IMenuRepository
{
    Task<GetListPagingResponse> GetListPaging(MenuGetListDto request);
    List<MenuModel> GetAll();
    Task<MenuModel> GetById(GetByIdRequest request);
    Task<MenuDto> GetByPost(GetByIdRequest request);
    Task<bool> Insert(MenuDto request);
    Task<bool> Update(MenuDto request);
}