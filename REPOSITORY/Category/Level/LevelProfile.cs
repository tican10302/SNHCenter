using AutoMapper;
using DAL.Entities;
using DTO.Category.Level.Dtos;
using DTO.Category.Level.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Category
{
    public class LevelProfile : Profile
    {
        public LevelProfile()
        {
            CreateMap<Level, LevelDto>();
            CreateMap<Level, LevelDto>();
            CreateMap<LevelDto, Level>();
            CreateMap<LevelModel, Level>();
            CreateMap<Level, LevelModel>();

        }
    }
}
