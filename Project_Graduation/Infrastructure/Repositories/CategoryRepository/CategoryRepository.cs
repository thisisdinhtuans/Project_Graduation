using System;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.CategoryRepository;

public class CategoryRepository:BaseRepository<Category>, ICategoryRepository
{

    public CategoryRepository(Project_Graduation_Context dbContext, IAuditRepository<Category> auditRepository) : base(dbContext, auditRepository)
    {
    }
}
