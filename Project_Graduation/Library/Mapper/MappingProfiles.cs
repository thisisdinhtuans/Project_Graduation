using System;
using AutoMapper;
using Domain.Models.Dto.Area;
using Domain.Models.Dto.Blog;
using Domain.Models.Dto.Category;
using Domain.Models.Dto.Restaurant;
using Domain.Models.Dto.Table;
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

    }
}
