using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Table;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.TableService
{
    public interface ITableService
    {
        Task<ApiResult<List<TableDto>>> GetAllTablesAsync();
        Task<ApiResult<Table>> GetTableByIdAsync(int id);
        Task<ApiResult<bool>> CreateTableAsync(CreateTableDto restaurantDto);
        Task<ApiResult<bool>> UpdateTableAsync(TableDto restaurantDto);
        Task<ApiResult<bool>> DeleteTableAsync(int id);
    }
}
