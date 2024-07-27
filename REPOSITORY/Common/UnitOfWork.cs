using System.Data;
using AutoMapper;
using DAL.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace REPOSITORY.Common;
public class UnitOfWork : IUnitOfWork
{
    private  DataContext _dataContext;
    private  DbContext _dbContext;
     private  Dictionary<Type, object> _repositories;
     private IDbContextTransaction _transaction;
     private bool disposed = false;
     public UnitOfWork(DataContext dbContext, DataContext dataContext)
     {
         _dataContext = dataContext;
         _dbContext = dbContext;
         _repositories = new Dictionary<Type, object>();
     }
     public IRepository<T> GetRepository<T>() where T : class
     {
         if(_repositories.ContainsKey(typeof(T)))
         {
             return _repositories[typeof(T)] as IRepository<T>;
         }

         var repository = new Repository<T>(_dbContext, _dataContext);
         _repositories.Add(typeof(T), repository);
         return repository;
     }
     public async Task BeginTransactionAsync()
     {
         _transaction = await _dataContext.Database.BeginTransactionAsync();
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
         return await _dataContext.SaveChangesAsync();
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
                 _dataContext.Dispose();
             }    
         }
         this.disposed = true;
     }
}