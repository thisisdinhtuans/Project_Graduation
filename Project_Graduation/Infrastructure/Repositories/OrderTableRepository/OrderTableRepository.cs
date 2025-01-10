using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.OrderTableRepository
{
    public class OrderTableRepository:BaseRepository<OrderTable>, IOrderTableRepository
    {
        private readonly Project_Graduation_Context _context;
        public OrderTableRepository(Project_Graduation_Context dbContext, IAuditRepository<OrderTable> auditRepository) : base(dbContext, auditRepository)
        {
            _context = dbContext;
        }

        public async Task<List<int>> GetTableIdsByOrderIdAsync(int orderId)
        {
            return await _context.OrderTables
                .Where(ot => ot.OrderId == orderId)
                .Select(ot => ot.TableID)
                .ToListAsync();
        }

        // Lấy danh sách các Table theo TableIds
        public async Task<List<Table>> GetTablesByIdsAsync(List<int> tableIds)
        {
            return await _context.Tables
                .Where(t => tableIds.Contains(t.TableID))
                .ToListAsync();
        }

    }

}