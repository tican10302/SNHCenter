using DTO.Base;
using DTO.Category.Level.Models;
using DTO.Category.Level.Dtos;

namespace REPOSITORY.Category.Level
{
    public interface ILevelRepository
    {
        Task<GetListPagingResponse> GetListPaging(LevelGetListDto request);
        Task<LevelDto> GetByPost(GetByIdRequest request);
        Task<LevelModel> GetById(GetByIdRequest request);
        Task<bool> Insert(LevelDto request);
        Task<bool> Update(LevelDto request);
        Task<bool> DeLeteList(DeleteListRequest request);
        List<ComboboxModel> GetAllForCombobox();
    }
}
