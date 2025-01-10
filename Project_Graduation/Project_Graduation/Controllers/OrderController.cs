using System;
using Domain.Enums;
using Domain.Models.Dto.Order;
using Infrastructure.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Project_Graduation.Controllers;

public class OrderController : BaseApiController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [Authorize(Roles = "Customer,Waiter")]
    [HttpDelete("{orderId}/orderDetails/{orderDetailId}/dishes/{dishId}")]
    public async Task<IActionResult> DeleteDishFromOrderDetail(int orderId, int orderDetailId, int dishId)
    {
        var result = await _orderService.DeleteDishFromOrderDetail(orderId, orderDetailId, dishId);
        if (result.IsSuccessed)
        {
            return Ok(result.ResultObj);
        }
        return BadRequest(result.Message);
    }
    [Authorize(Roles = "Customer,Guest")]
    [HttpPost("createByCustomer")]
    public async Task<IActionResult> CreateByCustomer([FromBody] OrderDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _orderService.CreateOrderByCustomer(request);
        if (!result.IsSuccessed == true) return BadRequest();
        return Ok(result);
    }

    [Authorize(Roles = "Receptionist")]
    [HttpPost("createByReceptionist")]
    public async Task<IActionResult> CreateByReceptionist([FromBody] OrderDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _orderService.CreateOrderByCustomer(request);
        if (!result.IsSuccessed == true) return BadRequest();
        return Ok(result);
    }

    [Authorize(Roles = "Customer,Waiter")]
    [HttpPost("update-order-details")]
    public async Task<IActionResult> UpdateOrderDetails([FromBody] OrderDetailUpdateRequest request)
    {
        try
        {
            Console.WriteLine($"Received OrderDetailUpdateRequest: {JsonConvert.SerializeObject(request)}");
            var result = await _orderService.UpdateOrderDetailsAsync(request);
            if (result)
            {
                return Ok("Chi tiết order đã được cập nhật thành công.");
            }
            else
            {
                return BadRequest("Không thể cập nhật chi tiết order.");
            }
        }
        catch (Exception ex)
        {
            // Log the exception details
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
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
        var result = await _orderService.ArrangeTableToOrder(orderId, request.TableId);
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
    public async Task<IActionResult> ViewOrderHistory(Guid userId)
    {
        var result = await _orderService.ViewOrderHistory(userId);
        if (result.IsSuccessed)
        {
            return Ok(result.ResultObj);
        }
        return BadRequest(result.Message);
    }
//[HttpPost("create")]
//    [Authorize]
//    public async Task<IActionResult> AddOrder([FromBody] OrderDto request)
//    {
//        // Kiểm tra ModelState
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }

//        // Lấy tên người dùng từ HttpContext (JWT token)
//        var userName = HttpContext.User.Identity.Name;

//        var result = await _orderService.AddOrder(request, userName, UserId);
//        if (!result.IsSuccessed == true) return BadRequest();
//        return Ok(result);
//    }
}
