using AutoMapper;
using DAL.Entities;
using DTO.Category.Program.Dtos;
using DTO.Category.Program.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Category
{
    public  class ProgramProfile : Profile
    {
        public ProgramProfile()
        {
            CreateMap<Program, ProgramDto>();
            CreateMap<ProgramDto, Program>();
            CreateMap<ProgramModel, Program>();
            CreateMap<Program, ProgramModel>();
            
        }
    }
}
