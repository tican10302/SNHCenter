using AutoMapper;
using DAL.Entities;
using DTO.Category.Shift;

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
