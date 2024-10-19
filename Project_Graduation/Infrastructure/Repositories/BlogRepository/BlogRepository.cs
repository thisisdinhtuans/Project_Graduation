using System;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.BlogRepository;

public class BlogRepository:BaseRepository<Blog>, IBlogRepository
{

    public BlogRepository(Project_Graduation_Context dbContext, IAuditRepository<Blog> auditRepository) : base(dbContext, auditRepository)
    {
    }
}
