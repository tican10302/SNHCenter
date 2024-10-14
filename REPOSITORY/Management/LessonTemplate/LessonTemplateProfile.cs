using AutoMapper;
using DTO.Management.LessonTemplate.Dtos;
using DTO.Management.LessonTemplate.Models;

namespace REPOSITORY.Management.LessonTemplate;

public class LessonTemplateProfile : Profile
{
    public LessonTemplateProfile()
    {
        CreateMap<DAL.Entities.LessonTemplate, LessonTemplateDto>();
        CreateMap<LessonTemplateDto, DAL.Entities.LessonTemplate>();
        CreateMap<DAL.Entities.LessonTemplate, LessonTemplateModel>();
        CreateMap<LessonTemplateModel, DAL.Entities.LessonTemplate>();
    }
}