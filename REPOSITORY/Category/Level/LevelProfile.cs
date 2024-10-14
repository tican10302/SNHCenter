using AutoMapper;
using DTO.Category.Level.Dtos;
using DTO.Category.Level.Models;

namespace REPOSITORY.Category.Level
{
    public class LevelProfile : Profile
    {
        public LevelProfile()
        {
            CreateMap<DAL.Entities.Level, LevelDto>();
            CreateMap<DAL.Entities.Level, LevelDto>();
            CreateMap<LevelDto, DAL.Entities.Level>();
            CreateMap<LevelModel, DAL.Entities.Level>();
            CreateMap<DAL.Entities.Level, LevelModel>();

        }
    }
}