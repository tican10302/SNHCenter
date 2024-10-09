using DTO.Base;
using DTO.Training.Course.Models;
using DTO.Training.Course.Dtos;

namespace REPOSITORY.Category.Course
{
    public interface ICourseRepository
    {
        Task<GetListPagingResponse> GetListPaging(CourseGetListDto request);
        Task<CourseDto> GetByPost(GetByIdRequest request);
        Task<CourseModel> GetById(GetByIdRequest request);
        Task<bool> Insert(CourseDto request);
        Task<bool> Update(CourseDto request);
        Task<bool> DeLeteList(DeleteListRequest request);
        List<ComboboxModel> GetAllForCombobox();
    }
}
