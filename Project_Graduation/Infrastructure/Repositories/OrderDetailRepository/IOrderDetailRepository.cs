using System;
using Infrastructure.Entities;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.OrderDetailRepository;

public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
{
    Task<OrderDetail> GetByDishIdAndOrderId(int dishId, int orderId);
}
