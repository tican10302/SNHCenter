using System.Data;
using AutoMapper;
using DAL.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace REPOSITORY.Common;
public class UnitOfWork : IUnitOfWork
{
     private  DataContext _dbContext;
     private  Dictionary<Type, object> _repositories;
     private IDbContextTransaction _transaction;
     private IMapper _mapper;
     private bool disposed = false;
     public UnitOfWork(DataContext dbContext)
     {
         _dbContext = dbContext;
         _repositories = new Dictionary<Type, object>();
     }
     public IRepository<T> GetRepository<T>() where T : class
     {
         if(_repositories.ContainsKey(typeof(T)))
         {
             return _repositories[typeof(T)] as IRepository<T>;
         }

         var repository = new Repository<T>(_dbContext);
         _repositories.Add(typeof(T), repository);
         return repository;
     }
     public async Task<List<T>> ExecWithStoreProcedure<T>(string query, params SqlParameter[] parameters) where T : class
     {

         try
            {
                var parametersList = parameters ?? Array.Empty<SqlParameter>();
                var sql = $"exec {query} {string.Join(", ", parametersList.Select(p => p.ParameterName))}";
                
                var outputParam = parametersList.FirstOrDefault(p => p.Direction == ParameterDirection.Output);
                if (outputParam != null)
                {
                    sql += $" OUTPUT";
                }

                List<T> result = await _dbContext.Set<T>()
                                       .FromSqlRaw(sql, parametersList)
                                       .ToListAsync();
                
                if (outputParam != null)
                {
                    if (outputParam.Value != DBNull.Value)
                    {
                        var outputValue = Convert.ToInt64(outputParam.Value);
                    }
                }
                
                return result;
            }
         catch (Exception ex)
            {
                throw new Exception("An error occurred while executing the stored procedure: " + ex.Message);
            }
     }
     public async Task BeginTransactionAsync()
     {
         _transaction = await _dbContext.Database.BeginTransactionAsync();
     }

     public async Task CommitAsync()
     {
         try
         {
             await _transaction.CommitAsync();
         }
         catch
         {
             await _transaction.RollbackAsync();
             throw;
         }
         finally
         {
             await _transaction.DisposeAsync();
             _transaction = null!;
         }
     }

     public async Task RollbackAsync()
     {
         await _transaction.RollbackAsync();
         await _transaction.DisposeAsync();
         _transaction = null!;
     }

     public async Task<int> SaveChangesAsync()
     {
         return await _dbContext.SaveChangesAsync();
     }

     public void Dispose()
     {
         Dispose(true);
         GC.SuppressFinalize(this);
     }

     protected virtual void Dispose(bool disposing)
     {
         if (!this.disposed)
         {
             if(disposing)
             {
                 _dbContext.Dispose();
             }    
         }
         this.disposed = true;
     }
}