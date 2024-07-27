using System.Linq.Expressions;
using DAL.Entities;
using Microsoft.Data.SqlClient;

namespace REPOSITORY;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> Find(Expression<Func<T, bool>> match);
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, string[] includes = null);
    Task<List<T>> ExecWithStoreProcedure(string query, params SqlParameter[] parameters);
}