using AutoMapper;
using DAL.Entities;
using DTO.Category.Province.Dtos;
using DTO.Category.Province.Models;

namespace REPOSITORY.Category.Province;

public class ProvinceProfile : Profile
{
    public ProvinceProfile()
    {
        CreateMap<Provinces, ProvinceDto>();
        CreateMap<ProvinceDto, Provinces>();
        CreateMap<ProvinceModel, Provinces>();
        CreateMap<Provinces, ProvinceModel>();
    }
}