using AutoMapper;
using DTO.System.Account.Models;
using DTO.System.GroupPermission.Dtos;

namespace REPOSITORY.System.GroupPermission
{
    public class GroupPermissionProfile : Profile
    {
        public GroupPermissionProfile()
        {
            CreateMap<GroupPermissionModel, DAL.Entities.GroupPermission>();
            CreateMap<DAL.Entities.GroupPermission, GroupPermissionModel>();
            CreateMap<GroupPermissionDto, DAL.Entities.GroupPermission>();
            CreateMap<DAL.Entities.GroupPermission, GroupPermissionDto>();
        }
    }
}
