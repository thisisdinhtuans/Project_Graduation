using System;

namespace Infrastructure.Repositories.AuditRepository;

public interface IAuditRepository<T> where T : class
{
    void SetAuditForCreate(T entity);
    void SetAuditForUpdate(T entity);
}
