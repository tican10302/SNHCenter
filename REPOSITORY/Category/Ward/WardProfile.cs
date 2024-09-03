using AutoMapper;
using DAL.Entities;
using DTO.Category.Ward.Dtos;
using DTO.Category.Ward.Models;

namespace REPOSITORY.Category.Ward;

public class WardProfile : Profile
{
    public WardProfile()
    {
        CreateMap<Wards, WardDto>();
        CreateMap<WardDto, Wards>();
        CreateMap<WardModel, Wards>();
        CreateMap<Wards, WardModel>();
    }
}