using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DTO.Base;
using DTO.System.Menu.Dtos;
using DTO.System.Menu.Models;
using REPOSITORY.Common;

namespace REPOSITORY.System.Menu;

[RegisterClassAsTransient]
public class MenuRepository(IUnitOfWork unitOfWork, IMapper mapper) : IMenuRepository
{
    public async Task<BaseResponse<List<MenuModel>>> GetList(GetAllRequest request)
    {
        var response = new BaseResponse<List<MenuModel>>();

        try
        {
            var result = unitOfWork.GetRepository<DAL.Entities.Menu>()
                .GetAll(x => x.IsActived || !x.IsActived)
                .OrderBy(x => x.Sort)
                .ToList();

            response.Data = mapper.Map<List<MenuModel>>(result);
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }
    
    public async Task<BaseResponse<List<MenuModel>>> GetAll(GetAllRequest request)
    {
        var response = new BaseResponse<List<MenuModel>>();

        try
        {
            var result = unitOfWork.GetRepository<DAL.Entities.Menu>()
                .GetAll(x => x.IsActived)
                .OrderBy(x => x.Sort)
                .ToList();

            response.Data = mapper.Map<List<MenuModel>>(result);
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<MenuModel>> GetById(GetByIdRequest request)
    {
        var response = new BaseResponse<MenuModel>();

        try
        {
            var data = await unitOfWork.GetRepository<DAL.Entities.Menu>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }

            var result = mapper.Map<MenuModel>(data);
            response.Data = result;
        }
        catch(Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<MenuDto>> GetByPost(GetByIdRequest request)
    {
        var response = new BaseResponse<MenuDto>();

        try
        {
            var result = new MenuDto();
            var data = await unitOfWork.GetRepository<DAL.Entities.Menu>().GetByIdAsync(request.Id);
            if (result == null)
            {
                result.Id = Guid.NewGuid();
                result.IsEdit = false;
            }
            else
            {
                result = mapper.Map<MenuDto>(data);
                result.IsEdit = true;
            }

            response.Data = result;

        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<MenuModel>> Insert(MenuDto request)
    {
        var response = new BaseResponse<MenuModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.Menu>().Find(x => 
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }
            
            var entity = mapper.Map<DAL.Entities.Menu>(request);
            var result = await unitOfWork.GetRepository<DAL.Entities.Menu>().AddAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            
            response.Data = mapper.Map<MenuModel>(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<MenuModel>> Update(MenuDto request)
    {
        var response = new BaseResponse<MenuModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.Menu>().Find(x =>
                x.Id != request.Id &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }

            var data = await unitOfWork.GetRepository<DAL.Entities.Menu>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }
            var entity = mapper.Map(request, data);
            await unitOfWork.GetRepository<DAL.Entities.Menu>().UpdateAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            response.Data = mapper.Map<MenuModel>(entity);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    
}