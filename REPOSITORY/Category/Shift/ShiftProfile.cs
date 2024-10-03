using AutoMapper;
using DAL.Entities;
using DTO.Category.Shift.Dtos;
using DTO.Category.Shift.Models;
using System.Data;

namespace REPOSITORY.Category
{
    public class ShiftProfile : Profile
    {
        public ShiftProfile()
        {
            CreateMap<DAL.Entities.Program, ShiftDto>();
            CreateMap<ShiftDto, DAL.Entities.Program>();
            CreateMap<ShiftModel, DAL.Entities.Program>();
            CreateMap<DAL.Entities.Program, ShiftModel>();
            CreateMap<DataRow, ShiftModel>()
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src =>
                src["Days"] != DBNull.Value
                ? src["Days"].ToString().Split(new[] { ',' }, StringSplitOptions.None).Select(day => day.Trim()).ToList()
                : new List<string>()));
        }
    }
}
