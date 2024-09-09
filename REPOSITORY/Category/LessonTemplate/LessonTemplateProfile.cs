using AutoMapper;
using DTO.Category.LessonTemplate.Dtos;
using DTO.Category.LessonTemplate.Models;

namespace REPOSITORY.Category.LessonTemplate;

public class LessonTemplateProfile : Profile
{
    public LessonTemplateProfile()
    {
        CreateMap<DAL.Entities.LessonTemplate, LessonTemplateModel>();
        CreateMap<LessonTemplateModel, DAL.Entities.LessonTemplate>();
        CreateMap<DAL.Entities.LessonTemplate, LessonTemplateDto>();
        CreateMap<LessonTemplateDto, DAL.Entities.LessonTemplate>();
    }
}