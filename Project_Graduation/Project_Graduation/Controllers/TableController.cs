using AutoMapper;
using Domain.Models.Dto.Table;
using Infrastructure.Entities;
using Infrastructure.Services.TableService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers
{
    public class TablesController : BaseApiController
    {
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        public TablesController(ITableService tableService, IMapper mapper)
        {
            _tableService = tableService;
            _mapper = mapper;
        }

        // [HttpGet]
        // public async Task<ActionResult<PagedList<Table>>> GetTables([FromQuery] TableParams tableParams)
        // {
        //     var tables = await _tableService.GetTablesAsync(tableParams);
        //     Response.AddPaginationHeader(tables.MetaData);
        //     return Ok(tables);
        // }

        [HttpGet("get-full")]
        public async Task<IActionResult> GetAllTables()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables); // Tr? v? danh sách các nhà hàng
        }

        [HttpGet("{id}", Name = "GetTable")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null) return NotFound();
            return Ok(table);
        }


        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateTable([FromBody] CreateTableDto tableDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _tableService.CreateTableAsync(tableDto);
            //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi thêm nhà hàng" });
            //return NoContent();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateTable([FromBody] TableDto tableDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _tableService.UpdateTableAsync(tableDto);
            //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi cập nhật nhà hàng" });
            //return NoContent();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTable(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _tableService.DeleteTableAsync(id);
            //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi xóa nhà hàng" });
            //return NoContent();
            return Ok(result);
        }
    }
}
