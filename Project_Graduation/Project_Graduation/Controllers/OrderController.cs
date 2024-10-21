using System;
using Domain.Enums;
using Domain.Models.Dto.Order;
using Infrastructure.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class OrderController : BaseApiController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [AllowAnonymous]
        [HttpPost("post")]
        public async Task<IActionResult> Create([FromBody] OrderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.CreateOrder(request);
            if (!result.IsSuccessed == true) return BadRequest();
            return Ok(result);
        }
        [Authorize(Roles = "Customer,Receptionist,Waiter")]
        [HttpPut("put")]
        public async Task<IActionResult> Update([FromBody] OrderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.UpdateOrder(request);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [Authorize(Roles = "Customer,Receptionist,Waiter")]
        [HttpDelete("remove")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.DeleteOrder(id);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [Authorize(Roles = "Customer,Manager,Owner,Receptionist,Waiter")]
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.GetOrderById(id);
            if (result == null) return BadRequest();

            return Ok(result);
        }

        [Authorize(Roles = "Customer,Manager,Owner,Receptionist,Waiter")]
        [HttpGet("get-all-order")]
        public async Task<IActionResult> GetAllOrder()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.GetAllOrder();
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [Authorize(Roles = "Receptionist,Waiter")]
        [HttpPut("UpdateStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] EnumOrder newStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.UpdateOrderStatus(orderId, newStatus);
            return Ok(result);
        }

        [Authorize(Roles = "Receptionist")]
        [HttpPut("{orderId}/assign-table/{tableId}")]
        public async Task<IActionResult> ArrangeTableToOrder(int orderId, int tableId)
        {
            try
            {
                var result = await _orderService.ArrangeTableToOrder(orderId, tableId);
                if (result)
                {
                    return Ok("Bàn đã được gán thành công cho order.");
                }
                else
                {
                    return BadRequest("Không thể gán bàn cho order.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [Authorize(Roles = "Receptionist")]
        [HttpPost("{orderId}/AssignTable")]
        public async Task<IActionResult> AssignTableToOrder(int orderId, [FromBody] AssignTableRequest request)
        {
            var result = await _orderService.AssignTableToOrder(orderId, request.TableId);
            if (result)
            {
                return Ok(new { success = true, message = "Bàn đã được gán thành công" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Không thể gán bàn cho order." });
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("ViewOrderHistory/{username}")]
        public async Task<IActionResult> ViewOrderHistory(string username)
        {
            var result = await _orderService.ViewOrderHistory(username);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObj);
            }
            return BadRequest(result.Message);
        }
}
