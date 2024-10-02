using AutoMapper;
using DTO.Category.Program.Dtos;
using DTO.Category.Program.Models;

namespace REPOSITORY.Category.Program
{
    public  class ProgramProfile : Profile
    {
        public ProgramProfile()
        {
            CreateMap<DAL.Entities.Program, ProgramDto>();
            CreateMap<ProgramDto, DAL.Entities.Program>();
            CreateMap<ProgramModel, DAL.Entities.Program>();
            CreateMap<DAL.Entities.Program, ProgramModel>();
            
        }
    }
}