using AutoMapper;
using DTO.System.Role.Dtos;
using DTO.System.Role.Models;

namespace REPOSITORY.System.Role
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<DAL.Entities.Role, RoleDto>();
            CreateMap<RoleDto, DAL.Entities.Role>();
            CreateMap<RoleModel, DAL.Entities.Role>();
            CreateMap<DAL.Entities.Role, RoleModel>();
            
            CreateMap<DAL.Entities.Permission, Role_PermissionModel>();
            CreateMap<Role_PermissionModel, DAL.Entities.Permission>();
            CreateMap<Role_PermissionDto, DAL.Entities.Permission>();
            CreateMap<DAL.Entities.Permission, Role_PermissionDto>();
        }
    }
}
