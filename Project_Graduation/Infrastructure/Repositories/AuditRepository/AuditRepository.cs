using System;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repositories.AuditRepository;

public class AuditRepository<T> : IAuditRepository<T> where T : BaseEntity
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetAuditForCreate(T entity)
    {
        var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
        entity.CreatedBy = userName;
        entity.CreatedDate = DateTime.Now;
    }

    public void SetAuditForUpdate(T entity)
    {
        var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
        entity.UpdatedBy = userName;
        entity.UpdatedDate = DateTime.Now;
    }
}
