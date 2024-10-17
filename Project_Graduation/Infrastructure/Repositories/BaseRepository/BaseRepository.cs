﻿using System;
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

    public async Task<bool> AddAsync(T entity)
    {
        _auditRepository.SetAuditForCreate(entity);
        await _dbSet.AddAsync(entity);
        return await SaveAllAsync();
    }

    public async Task<bool> Update(T entity)
    {
        _auditRepository.SetAuditForUpdate(entity);
        _dbSet.Update(entity);
        return await SaveAllAsync();
    }

    public async Task<bool> Delete(T entity)
    {
        _dbSet.Remove(entity);
        return await SaveAllAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        var saved = await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync() để lưu dữ liệu bất đồng bộ
        return saved > 0;
    }


    public async Task<List<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }
}