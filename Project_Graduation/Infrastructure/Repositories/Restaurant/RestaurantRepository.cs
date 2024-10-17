using System;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Entities;

public class RestaurantRepository:BaseRepository<Restaurant>, IRestaurantRepository
{

    public RestaurantRepository(Project_Graduation_Context dbContext, IAuditRepository<Restaurant> auditRepository) : base(dbContext, auditRepository)
    {
    }
}
