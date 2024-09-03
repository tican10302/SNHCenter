using AutoMapper;
using DAL.Entities;
using DTO.Management.Teacher.Dtos;
using DTO.Management.Teacher.Models;

namespace REPOSITORY.Management;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<User, TeacherDto>();
        CreateMap<TeacherDto, User>();
        CreateMap<TeacherModel, User>();
        CreateMap<User, TeacherModel>();
    }
}