using AutoMapper;
using DTO.Category.Shift.Dtos;
using DTO.Category.Shift.Models;
using System.Data;

namespace REPOSITORY.Category
{
    public class ShiftProfile : Profile
    {
        public ShiftProfile()
        {
            CreateMap<DAL.Entities.Shift, ShiftDto>();
            CreateMap<ShiftDto, DAL.Entities.Shift>();
            CreateMap<ShiftModel, DAL.Entities.Shift>();
            CreateMap<DAL.Entities.Shift, ShiftModel>();
        }
    }
}
