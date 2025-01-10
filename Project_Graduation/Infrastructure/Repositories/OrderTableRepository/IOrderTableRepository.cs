using Infrastructure.Entities;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.OrderTableRepository
{
    public interface IOrderTableRepository: IBaseRepository<OrderTable>
    {
         Task<List<int>> GetTableIdsByOrderIdAsync(int orderId);
        Task<List<Table>> GetTablesByIdsAsync(List<int> tableIds);
    }
}