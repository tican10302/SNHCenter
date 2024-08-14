using AutoMapper;
using DAL.Entities;
using DTO.Category.Shift.Dtos;
using DTO.Category.Shift.Requests;
using System.Data;

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
            CreateMap<DataRow, ShiftModel>()
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src =>
                src["Days"] != DBNull.Value
                ? src["Days"].ToString().Split(new[] { ',' }, StringSplitOptions.None).Select(day => day.Trim()).ToList()
                : new List<string>()));
        }
    }
}
