using System;
using AutoMapper;
using Domain.Models.Dto.Area;
using Domain.Models.Dto.Blog;
using Domain.Models.Dto.Category;
using Domain.Models.Dto.Dish;
using Domain.Models.Dto.Order;
using Domain.Models.Dto.OrderDetails;
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

            CreateMap<AppUser, UserRequestDto>();
            CreateMap<UserRequestDto, AppUser>();

            CreateMap<AppUser, StaffCreateDto>();
            CreateMap<StaffCreateDto, AppUser>();

            CreateMap<OrderDetailUpdateRequest, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<OrderDetail,OrderDetailDto>();

            CreateMap<OrderDto, Order>();
            CreateMap<Order,OrderDto>();

            CreateMap<OrderDetailUpdateRequest ,OrderDetail>();
            CreateMap<OrderDetail ,OrderDetailUpdateRequest>();


    }
}
