using System;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.AreaRepository;

public class AreaRepository:BaseRepository<Area>, IAreaRepository
{

    public AreaRepository(Project_Graduation_Context dbContext, IAuditRepository<Area> auditRepository) : base(dbContext, auditRepository)
    {
    }
}