using System;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.DishRepository;

public class DishRepository:BaseRepository<Dish>, IDishRepository
{

    public DishRepository(Project_Graduation_Context dbContext, IAuditRepository<Dish> auditRepository) : base(dbContext, auditRepository)
    {
    }
}
