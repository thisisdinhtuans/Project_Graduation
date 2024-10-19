using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.TableRepository;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TableRepository
{
    public class TableRepository : BaseRepository<Table>, ITableRepository
    {

        public TableRepository(Project_Graduation_Context dbContext, IAuditRepository<Table> auditRepository) : base(dbContext, auditRepository)
        {
        }
    }
}
