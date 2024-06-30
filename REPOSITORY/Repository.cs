using System.Linq.Expressions;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace REPOSITORY;
public class Repository<T>(DbContext dbContext) : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
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
    
    public async Task<T> Find(Expression<Func<T, bool>> match)
    {
        return await _dbSet.FirstOrDefaultAsync(match);
    }
    
    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, string[] includes = null)
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
}