using System;
using AutoMapper;
using Domain.Models.Dto.Area;
using Domain.Models.Dto.Blog;
using Domain.Models.Dto.Category;
using Domain.Models.Dto.Dish;
using Domain.Models.Dto.Order;
using Domain.Models.Dto.OrderDetails;
using Domain.Models.Dto.OrderTable;


// using Domain.Models.Dto.OrderDetails;
using Domain.Models.Dto.Restaurant;
using Domain.Models.Dto.Staff;
using Domain.Models.Dto.Table;
using Domain.Models.Dto.User;
using Infrastructure.Entities;

namespace Library.Mapper;

public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateRestaurantDto, Restaurant>();
            CreateMap<RestaurantDto, Restaurant>();
            CreateMap<Restaurant,RestaurantDto>();

            CreateMap<CreateAreaDto, Area>();
            CreateMap<AreaDto, Area>();
            CreateMap<Area,AreaDto>();

            CreateMap<CreateTableDto, Table>();
            CreateMap<TableDto, Table>();
            CreateMap<Table, TableDto>();

            CreateMap<CreateBlogDto, Blog>();
            CreateMap<BlogDto, Blog>();
            CreateMap<Blog, BlogDto>();

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateDishDto, Dish>();
            CreateMap<DishDto, Dish>();
            CreateMap<Dish, DishDto>();

            CreateMap<User, UserRequestDto>();
            CreateMap<UserRequestDto, User>();

            CreateMap<User, StaffCreateDto>();
            CreateMap<StaffCreateDto, User>();

            CreateMap<OrderDetailUpdateDto, OrderDto>();
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<OrderDetail,OrderDetailDto>();

            CreateMap<OrderDto, Order>();
            CreateMap<Order,OrderDto>();
            CreateMap<OrderTableDto, OrderTable>();
            CreateMap<OrderTable,OrderTableDto>();

            CreateMap<OrderDetailUpdateDto ,OrderDetail>();
            CreateMap<OrderDetail ,OrderDetailUpdateDto>();


    }
}
