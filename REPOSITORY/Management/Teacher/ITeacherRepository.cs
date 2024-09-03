using DTO.Base;
using DTO.Management.Teacher.Dtos;
using DTO.Management.Teacher.Models;

namespace REPOSITORY.Management;

public interface ITeacherRepository
{
    Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
    Task<BaseResponse<TeacherModel>> GetById(GetByIdRequest request);
    Task<BaseResponse<TeacherDto>> GetByPost(GetByIdRequest request);
    Task<BaseResponse<TeacherModel>> Insert(TeacherDto request);
    Task<BaseResponse<TeacherModel>> Update(TeacherDto request);
    Task<BaseResponse<string>> DeLeteList(DeleteListRequest request);
}