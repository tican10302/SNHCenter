using AutoMapper;
using DTO.Management.CourseTemplate.Dtos;
using DTO.Management.CourseTemplate.Models;

namespace REPOSITORY.Management.CourseTemplate;

public class CourseTemplateProfile : Profile
{
    public CourseTemplateProfile()
    {
        CreateMap<DAL.Entities.CourseTemplate, CourseTemplateDto>();
        CreateMap<CourseTemplateDto, DAL.Entities.CourseTemplate>();
        CreateMap<CourseTemplateModel, DAL.Entities.CourseTemplate>();
        CreateMap<DAL.Entities.CourseTemplate, CourseTemplateModel>();
    }
}