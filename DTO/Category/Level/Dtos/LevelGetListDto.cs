using DTO.Base;

namespace DTO.Category.Level.Dtos;

public class LevelGetListDto : GetListPagingRequest
{
    public Guid? ProgramId { get; set; }
}