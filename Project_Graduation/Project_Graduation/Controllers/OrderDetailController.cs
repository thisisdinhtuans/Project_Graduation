using System;
using Domain.Models.Dto.OrderDetails;
using Infrastructure.Services.OrderDetailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Project_Graduation.Controllers;

public class OrderDetailController : BaseApiController
{
    private readonly IOrderDetailService _orderDetailService;

    public OrderDetailController(IOrderDetailService orderDetailService)
    {
        _orderDetailService = orderDetailService;
    }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-all-order-detail-by-id")]
        public async Task<IActionResult> GetAllOrderDetail()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderDetailService.GetAllOrderDetailsAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [Authorize(Roles = "Customer,Manager,Owner,Receptionist,Waiter")]
        [HttpGet("get-all-order-detail-by-orderId")]
        public async Task<IActionResult> GetAllOrderDetailByOrderId(int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderDetailService.GetAllByOrderId(orderId);
            if (result == null) return BadRequest("Không tìm thấy chi tiết đơn hàng cho OrderID này.");
            return Ok(result);
        }

        [Authorize(Roles = "Customer,Waiter")]
        [HttpPost("update-order-details")]
        public async Task<IActionResult> UpdateOrderDetails([FromBody] OrderDetailUpdateRequest request)
        {
            try
            {
                Console.WriteLine($"Received OrderDetailUpdateRequest: {JsonConvert.SerializeObject(request)}");
                var result = await _orderDetailService.UpdateOrderDetailAsync(request);
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
}
