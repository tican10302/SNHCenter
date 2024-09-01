using DTO.Base;

namespace DTO.System.Menu.Dtos;

public class MenuGetListDto : GetListPagingRequest
{
    public Guid? GroupPermissionId { get; set; }
}