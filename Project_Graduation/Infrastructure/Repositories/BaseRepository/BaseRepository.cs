using System;
using System.Linq.Expressions;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.BaseRepository;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly Project_Graduation_Context _context;
    private readonly DbSet<T> _dbSet;
    
    private readonly IAuditRepository<T> _auditRepository;
    public BaseRepository(Project_Graduation_Context context, IAuditRepository<T> auditRepository)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _auditRepository = auditRepository;
    }
    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Add(T entity)
    {
        _auditRepository.SetAuditForCreate(entity);
        await _dbSet.AddAsync(entity);
        await SaveAllAsync();   
    }

    public async Task AddRange(List<T> entities)
    {
        foreach (var entity in entities)
        {
            _auditRepository.SetAuditForCreate(entity); // Set audit information for each entity in the range
        }
        await _dbSet.AddRangeAsync(entities); // Add range of entities
        await _context.SaveChangesAsync(); // Save changes to the database
    }

    public async Task Update(T entity)
    {
        _auditRepository.SetAuditForUpdate(entity);
        _dbSet.Update(entity);
        await SaveAllAsync();
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await SaveAllAsync();
    }

    public async Task SaveAllAsync()
    {
        await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync() để lưu dữ liệu bất đồng bộ
    }


    public async Task<List<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

    public async Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression)
    {
        var a = await _dbSet.Where(expression).AsNoTracking().ToListAsync();
        return a;
    }

}
