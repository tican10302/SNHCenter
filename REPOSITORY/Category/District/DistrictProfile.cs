using AutoMapper;
using DAL.Entities;
using DTO.Category.District.Dtos;
using DTO.Category.District.Models;

namespace REPOSITORY.Category.District;

public class DistrictProfile : Profile
{
    public DistrictProfile()
    {
        CreateMap<Districts, DistrictDto>();
        CreateMap<DistrictDto, Districts>();
        CreateMap<DistrictModel, Districts>();
        CreateMap<Districts, DistrictModel>();
    }
}