using DTO.Base;

namespace DTO.Training.Course.Dtos;

public class CourseGetListDto : GetListPagingRequest
{
    public Guid? ShiftId { get; set; }
    public Guid? LevelId { get; set; }
}