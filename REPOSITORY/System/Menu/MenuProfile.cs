using AutoMapper;
using DTO.System.Menu.Dtos;
using DTO.System.Menu.Models;

namespace REPOSITORY.System.Menu;

public class MenuProfile : Profile
{
    public MenuProfile()
    {
        CreateMap<DAL.Entities.Menu, MenuDto>();
        CreateMap<MenuDto, DAL.Entities.Menu>();
        CreateMap<DAL.Entities.Menu, MenuModel>();
        CreateMap<MenuModel, DAL.Entities.Menu>();
    }
}