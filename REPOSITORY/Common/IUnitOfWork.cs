using Microsoft.Data.SqlClient;

namespace REPOSITORY.Common;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : class;
    Task<List<T>> ExecWithStoreProcedure<T>(string query, params SqlParameter[] parameters) where T : class;
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}