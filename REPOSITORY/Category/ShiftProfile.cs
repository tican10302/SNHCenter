using AutoMapper;
using DAL.Entities;
using DTO.Category.Shift.Dtos;
using DTO.Category.Shift.Requests;

namespace REPOSITORY.Category
{
    public class ShiftProfile : Profile
    {
        public ShiftProfile()
        {
            CreateMap<Shift, ShiftDto>();
            CreateMap<ShiftDto, Shift>();
            CreateMap<ShiftModel, Shift>();
            CreateMap<Shift, ShiftModel>();
        }
    }
}
