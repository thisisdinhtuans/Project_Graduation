using System;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.BaseRepository;

public interface IBaseRepository<T>
{
    Task<List<T>> GetAll();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task SaveAllAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

}
