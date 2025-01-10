using AutoMapper;
using Domain.Models.Common.ApiResult;
using Domain.Models.Common;
using Domain.Models.Dto.Table;
using Infrastructure.Entities;
using Infrastructure.Repositories.TableRepository;
using Infrastructure.Services.TableService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories.AreaRepository;
using Domain.Enums;
using Infrastructure.Repositories.OrderTableRepository;

namespace Infrastructure.Services.TableService
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IOrderTableRepository _orderTableRepository;
        private readonly IMapper _mapper;

        public TableService(ITableRepository tableRepository,IOrderTableRepository orderTableRepository, IAreaRepository areaRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _areaRepository = areaRepository;
            _mapper = mapper;
            _orderTableRepository = orderTableRepository;
        }
        public async Task<ApiResult<bool>> CreateTableAsync(CreateTableDto tableDto)
        {
            var area = await _areaRepository.GetByIdAsync(tableDto.AreaID);
            if (area == null)
            {
                return new ApiErrorResult<bool>("Khu vực này không tồn tại");
            }
            if (tableDto == null)
            {
                throw new ArgumentNullException(nameof(tableDto));
            }

            //var addressExists = await _tableRepository.AnyAsync(x => x.TableNumber == tableDto.TableNumber);
            //if (addressExists)
            //{
            //    return new ApiErrorResult<bool>("Bàn  này đã tồn tại.");
            //}


            var table = _mapper.Map<Table>(tableDto);
            // table.CreatedBy=_httpContextAccessor.HttpContext.User.Identity.Name;
            // table.CreatedDate=DateTime.Now;
            try
            {
                await _tableRepository.Add(table);
                return new ApiSuccessResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }

        }

        public async Task<ApiResult<bool>> UpdateTableAsync(TableDto tableDto)
        {
            var addressExists = await _tableRepository.AnyAsync(x => x.TableNumber == tableDto.TableNumber);
            //if (addressExists)
            //{
            //    return new ApiErrorResult<bool>("Bàn này đã tồn tại.");
            //}

            var table = await _tableRepository.GetByIdAsync(tableDto.TableID);
            if (table == null) throw new Exception("Table not found");

            _mapper.Map(tableDto, table);
            // table.UpdatedDate = DateTime.Now;
            // table.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            try
            {
                await _tableRepository.Update(table);
                return new ApiSuccessResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> DeleteTableAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
            {
                return new ApiErrorResult<bool>("Bàn không được tìm thấy.");
            }

            try
            {
                await _tableRepository.Delete(table);
                return new ApiSuccessResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }


        public async Task<ApiResult<Table>> GetTableByIdAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            return new ApiSuccessResult<Table>(table);
        }

        public async Task<ApiResult<List<TableDto>>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllAsync(); // Lấy tất cả nhà hàng từ Repository
            var tablesDto = _mapper.Map<List<TableDto>>(tables); // Chuyển đổi sang DTO
            return new ApiSuccessResult<List<TableDto>>(tablesDto);
        }

        public async Task<ApiResult<bool>> UpdateStatusTable(int id, EnumTable status)
        {
            try
            {
                var table = await _tableRepository.GetByIdAsync(id);
                if (table == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy bàn");
                }
                table.Status = (int)status;
                await _tableRepository.Update(table);
                return new ApiSuccessResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>($"Lỗi khi cập nhật trạng thái: {ex.Message}");
            }
        }

        public async Task UpdateTableStatusByOrderIdAsync(int orderId, EnumTable status)
        {
            // Lấy danh sách TableId từ OrderTable
            var tableIds = await _orderTableRepository.GetTableIdsByOrderIdAsync(orderId);

            if (!tableIds.Any())
            {
                throw new Exception("No tables found for the given OrderId.");
            }

            // Lấy danh sách Table từ TableIds
            var tables = await _orderTableRepository.GetTablesByIdsAsync(tableIds);

            // Cập nhật trạng thái của từng Table
            foreach (var table in tables)
            {
                table.Status = (int)status;
            }

            // Lưu thay đổi
            await _orderTableRepository.SaveAllAsync();
        }
    }

}
