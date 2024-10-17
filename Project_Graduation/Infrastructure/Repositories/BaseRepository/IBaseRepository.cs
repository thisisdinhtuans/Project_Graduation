using System;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.BaseRepository;

public interface IBaseRepository<T>
{
    Task<List<T>> GetAll();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> AddAsync(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
    Task<bool> SaveAllAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

}
