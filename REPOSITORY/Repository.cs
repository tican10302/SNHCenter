using System.Data;
using System.Linq.Expressions;
using DAL.Data;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace REPOSITORY;
public class Repository<T>(DbContext dbContext, DataContext dataContext) : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var data = await _dbSet.FindAsync(id);
        return data ?? null;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<T?> Find(Expression<Func<T, bool>> match)
    {
        var data = await _dbSet.FirstOrDefaultAsync(match);
        return data ?? null;
    }
    
    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, string[]? includes = null)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query;
    }
    
    public async Task<List<T>> ExecWithStoreProcedure(string query, DynamicParameters parameters)
    {
        try
        {
            var connection = dataContext.Database.GetDbConnection();

            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }

            var result = await connection.QueryAsync<T>(
                sql: query,
                param: parameters,
                commandType: CommandType.StoredProcedure);

            return result.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while executing the stored procedure: " + ex.Message);
        }
    }
}