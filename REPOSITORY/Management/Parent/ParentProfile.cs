using AutoMapper;
using DTO.Management.Parent.Dtos;
using DTO.Management.Parent.Models;

namespace REPOSITORY.Management.Parent
{
    public class ParentProfile : Profile
    {
        public ParentProfile()
        {
            CreateMap<DAL.Entities.Parent, ParentDto>();
            CreateMap<ParentDto, DAL.Entities.Parent>();
            CreateMap<ParentModel, DAL.Entities.Parent>();
            CreateMap<DAL.Entities.Parent, ParentModel>();

        }
    }
}