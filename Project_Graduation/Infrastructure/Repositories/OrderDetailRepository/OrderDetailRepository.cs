using System;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.OrderDetailRepository;

public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly Project_Graduation_Context _dbContext;
        public OrderDetailRepository(Project_Graduation_Context dbContext, IAuditRepository<OrderDetail> auditRepository) : base(dbContext, auditRepository)
        {
            _dbContext = dbContext;
        }
        public async Task<OrderDetail> GetByDishIdAndOrderId(int dishId, int orderId)
        {
            return await _dbContext.OrderDetails
                .FirstOrDefaultAsync(od => od.DishId == dishId && od.OrderId == orderId);
        }

}