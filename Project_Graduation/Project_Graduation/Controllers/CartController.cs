using System;
using Domain.Models.Dto.OrderDetails;
using Infrastructure.Services.OrderDetailService;
using Infrastructure.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class CartController : BaseApiController
{
    private readonly IOrderDetailService _orderDetailService;

        public CartController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [Authorize(Roles = "Waiter,Customer,Guest")]
        [HttpPost("add-cart")]
        public async Task<IActionResult> Create([FromBody] OrderDetailDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderDetailService.CreateOrderDetailAsync(request);
                if (result.IsSuccessed)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [Authorize(Roles = "Waiter,Customer,Guest")]
        [HttpPut("update-cart")]
        public async Task<IActionResult> Update([FromBody] OrderDetailUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderDetailService.UpdateOrderDetailAsync(request);
                if (result)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [Authorize(Roles = "Waiter,Customer,Guest")]
        [HttpDelete("delete-cart")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderDetailService.DeleteOrderDetailAsync(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }

        [Authorize(Roles = "Waiter,Customer,Guest")]
        [HttpGet("get-by-id-cart")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderDetailService.GetOrderDetailByIdAsync(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        [Authorize(Roles = "Waiter,Customer,Guest")]
        [HttpGet("get-full-cart")]
        public async Task<IActionResult> GetFull(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderDetailService.GetAllByOrderId(id);
                return Ok(result);
            }
        }
    }

