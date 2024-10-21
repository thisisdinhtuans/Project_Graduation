using System;
using System.Security.Claims;
using AutoMapper;
using Domain.Enums;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Order;
using Domain.Models.Dto.OrderDetails;
using Infrastructure.Entities;
using Infrastructure.Repositories.OrderDetailRepository;
using Infrastructure.Repositories.OrderRepository;
using Infrastructure.Repositories.TableRepository;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderService(IOrderRepository orderRepository, IMapper mapper,IOrderDetailRepository orderDetailRepository, ITableRepository tableRepository,IHttpContextAccessor httpContextAccessor)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _orderDetailRepository = orderDetailRepository;
        _tableRepository = tableRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> ArrangeTableToOrder(int orderId, int tableId)
    {
        var order=await _orderRepository.GetByIdAsync(orderId);
        if(order==null)
        {
            throw new Exception("Order không tồn tại");
        }

        var table=await _tableRepository.GetByIdAsync(tableId);
        if(table==null)
        {
            throw new Exception("Bàn không tồn tại");
        }

        if(table.Status!=(int)EnumTable.Trong)
        {
            throw new Exception("Bàn không khả dụng");
        }
        order.TableID=tableId;
        await _orderRepository.Update(order);
        table.Status=(int)EnumTable.DaDat;
        await _tableRepository.Update(table);

        return true;
    }

    public async Task<bool> AssignTableToOrder(int orderId, int tableId)
    {
        var order=await _orderRepository.GetByIdAsync(orderId);
        if(order==null||order.TableID!=0)
        {
            return false;
        }

        var table=await _tableRepository.GetByIdAsync(tableId);
        if(table==null || table.Status!=0)
        {
            return false;
        }

        order.TableID=tableId;
        table.Status=(int)EnumTable.DaDat;
        await _orderRepository.Update(order);
        await _tableRepository.Update(table);
        return true;
    }

    public async Task<ApiResult<bool>> CreateOrder(OrderDto orderDto)
    {
        if (orderDto == null)
        {
            throw new ArgumentNullException(nameof(orderDto));
        }

        // var addressExists = await _orderRepository.AnyAsync(x => x.Address == orderDto.Address);
        // if (addressExists)
        // {
        //     return new ApiErrorResult<bool>("Nhà hàng với địa chỉ này đã tồn tại.");
        // }


        var order = new Order
    {
        RestaurantID = orderDto.RestaurantID,
        UserName = orderDto.UserName,
        PriceTotal = orderDto.PriceTotal,
        Description = orderDto.Description,
        NumberOfCustomer = orderDto.NumberOfCustomer,
        TableID = orderDto.TableID,
        Payment = orderDto.Payment,
        VAT = orderDto.VAT,
        Phone = orderDto.Phone,
        Date = orderDto.Date,
        Time = orderDto.Time,
        Status = orderDto.Status,
        Deposit = orderDto.Deposit,
        Discount = orderDto.Discount
    };

    // Lưu Order vào cơ sở dữ liệu trước
    await _orderRepository.Add(order);

    // Sau khi Order được lưu và OrderId được tạo, ta có thể gán nó cho OrderDetailDtos
    var orderDetails = orderDto.OrderDetailDtos.Select(x => new OrderDetail
    {
        UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, 
        OrderId = order.OrderId,  // Gán OrderId đã được tạo
        Price = x.Price,
        Description = x.Description,
        DishId = x.DishId,
        NumberOfCustomer = x.NumberOfCustomer,
        Quantity = x.Quantity
    }).ToList();

    // Lưu OrderDetails
    await _orderDetailRepository.AddRange(orderDetails);

    return new ApiSuccessResult<bool>(true);
    }

    // public async Task<ApiResult<bool>> DeleteDishFromOrderDetail(int orderId, int orderDetailId, int dishId)
    // {
    //     var result=await _orderRepository.DeleteDishFromOrderDetail(orderId,orderDetailId)
    // }

    public async Task<ApiResult<bool>> DeleteOrder(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return new ApiErrorResult<bool>("Nhà hàng không được tìm thấy.");
        }

        try
        {
            await _orderRepository.Delete(order);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    // public Task<ApiResult<List<OrderDetailDto>>> GetAllByOrderId(int orderId)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<ApiResult<List<OrderDto>>> GetAllOrder()
    {
        var orders = await _orderRepository.GetAllAsync(); // Lấy tất cả nhà hàng từ Repository
        var ordersDto=_mapper.Map<List<OrderDto>>(orders); // Chuyển đổi sang DTO
        return new ApiSuccessResult<List<OrderDto>>(ordersDto);
    }

    public async Task<ApiResult<OrderDto>> GetOrderById(int id)
    {
        var order= await _orderRepository.GetByIdAsync(id);
        var orderDto=_mapper.Map<OrderDto>(order);
        return new ApiSuccessResult<OrderDto>(orderDto);
    }

    public async Task<ApiResult<bool>> UpdateOrder(OrderDto orderDto)
    {
        // var addressExists = await _orderRepository.AnyAsync(x => x.Address == orderDto.Address);
        //     if (addressExists)
        //     {
        //         return new ApiErrorResult<bool>("Nhà hàng với địa chỉ này đã tồn tại.");
        //     }

        var order = await _orderRepository.GetByIdAsync(orderDto.OrderId);
        if (order == null) throw new Exception("Order not found");

        _mapper.Map(orderDto, order);
        // order.UpdatedDate = DateTime.Now;
        // order.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
        await _orderRepository.Update(order);
        try
        {
            await _orderRepository.Update(order);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> UpdateOrderStatus(int orderId, EnumOrder newStatus)
    {
        try
        { 
            var order=await _orderRepository.GetByIdAsync(orderId);
            if(order==null){
                return new ApiErrorResult<bool>("Đơn hàng này không tồn tại");
            }
            order.Status = (int)newStatus;

            await _orderRepository.Update(order);
            return new ApiSuccessResult<bool>(true);
        } catch(Exception ex)
        {
            return new ApiErrorResult<bool>($"Looix khi update: {ex.Message}");
        }
    }

    public async Task<ApiResult<List<OrderDto>>> ViewOrderHistory(string username)
    {
        var orders=await _orderRepository.GetByCondition(x=>x.UserName==username);
        var ordersDto=_mapper.Map<List<OrderDto>>(orders.ToList());
        return new ApiSuccessResult<List<OrderDto>>(ordersDto);
    }
}
