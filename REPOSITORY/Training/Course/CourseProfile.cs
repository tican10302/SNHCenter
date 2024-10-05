using AutoMapper;
using DTO.Training.Course.Dtos;
using DTO.Training.Course.Models;

namespace REPOSITORY.Category.Course
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<DAL.Entities.Course, CourseDto>();
            CreateMap<CourseDto, DAL.Entities.Course>();
            CreateMap<CourseModel, DAL.Entities.Course>();
            CreateMap<DAL.Entities.Course, CourseModel>();

        }
    }
}