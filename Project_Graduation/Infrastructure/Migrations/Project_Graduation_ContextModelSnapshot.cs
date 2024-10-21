﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(Project_Graduation_Context))]
    partial class Project_Graduation_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infrastructure.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("CCCD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Dob")
                        .HasMaxLength(200)
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RecoveryToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RestaurantID")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.Area", b =>
                {
                    b.Property<int>("AreaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AreaID"), 1L, 1);

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RestaurantID")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("AreaID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("Areas", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.Blog", b =>
                {
                    b.Property<int>("BlogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogID"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("SubTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BlogID");

                    b.ToTable("Blogs", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategory"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdCategory");

                    b.ToTable("Categories", (string)null);

                    b.HasData(
                        new
                        {
                            IdCategory = 1,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "HẢI SẢN"
                        },
                        new
                        {
                            IdCategory = 2,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "ĐỒ NƯỚNG"
                        },
                        new
                        {
                            IdCategory = 3,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "CÁ CÁC MÓN"
                        },
                        new
                        {
                            IdCategory = 4,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "MÓN ĂN CHƠI"
                        },
                        new
                        {
                            IdCategory = 5,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "MÓN NHẬU"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.Dish", b =>
                {
                    b.Property<int>("DishId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DishId"), 1L, 1);

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DishId");

                    b.HasIndex("CategoryID");

                    b.ToTable("Dishes", (string)null);

                    b.HasData(
                        new
                        {
                            DishId = 1,
                            CategoryID = 1,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Tên món: Tôm chiên hoàng kim\r\nĐịnh lượng: 1 suất 2-4 người ăn\r\nMô tả: Tôm chiên hoàng kim là một trong những món nhậu cực kỳ tốn bia tại Quán Nhậu Tự Do.\r\nTôm tươi, căng mọng đem chiên vàng giòn rồi lại đảo qua gia vị đậm đà tạo nên món ăn hấp dẫn, ăn một miếng là không ngừng lại được.",
                            Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/20240424094950598.webp",
                            Name = "Tôm chiên hoàng kim",
                            Price = 225000.0,
                            Type = "Suất"
                        },
                        new
                        {
                            DishId = 2,
                            CategoryID = 1,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Tên món: Tôm sú sốt ớt pattaya\r\nĐịnh lượng: 1 suất cho 3-4 người ăn\r\nMô tả: Tôm sú sốt ớt pattaya là một trong những món ăn mới lạ không thể bỏ qua khi ghé Quán Nhậu Tự Do.\r\nThịt tôm sú dai giòn ăn cùng rau củ, thấm đẫm nước sốt ớt pattaya “thần thánh\" xen lẫn vị chua chua, ngọt ngọt, cay nồng chắc chắn sẽ làm hài lòng mọi thực khách.",
                            Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/06/202406051717238266.webp",
                            Name = "Tôm sú sốt ớt pattaya",
                            Price = 225000.0,
                            Type = "Suất"
                        },
                        new
                        {
                            DishId = 3,
                            CategoryID = 2,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Tên món: Lợn mán nướng mắc khén\r\nĐịnh lượng: 1 suất cho 3-4 người ăn\r\nMô tả: Lợn mán nướng mắc khén là một trong những món nhậu kích thích vị giác của thực khách.\r\nThịt lợn mềm ngọt, có chút dai dai, không quá mỡ được nướng cùng hạt mắc khén thơm lừng. Sự hòa trộn giữa vị đậm đà của thịt lợn mán cùng các loại rau củ ăn kèm mang đến cho món ăn hương vị tuyệt hảo không thể nào quên.\r\n",
                            Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/202404231915283755.webp",
                            Name = "Lợn mán nướng mắc khén",
                            Price = 185000.0,
                            Type = "Suất"
                        },
                        new
                        {
                            DishId = 4,
                            CategoryID = 2,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Tên món: Gà đen nướng mắc khén\r\nĐịnh lượng: Gà nửa con, hạt mắc khén rang thơm, quả ớt cay, lá chanh, muối,củ sả.\r\nMô tả: Chuẩn gia vị chẩm chéo để chấm gà, thơm phức mùi mắc kén và tê tê đầu lưỡi.",
                            Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/05/20240530101506515.webp",
                            Name = "Gà đen nướng mắc khén",
                            Price = 300000.0,
                            Type = "Suất"
                        },
                        new
                        {
                            DishId = 5,
                            CategoryID = 5,
                            CreatedBy = "Admin",
                            CreatedDate = new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Tên món: Gà H’Mong rang muối\r\nĐịnh lượng: 800g\r\nMô tả: Gà H’Mong rang muối là món ăn được nhiều anh em sành ăn yêu thích tại Quán Nhậu Tự Do.\r\nGà H’Mong được tuyển chọn từ giống gà đen nuôi tự nhiên nên thịt rất săn chắc và thơm ngon. Đem gà chiên giòn và xóc đều với bột muối, sả, hành và lá chanh là đủ để có một món nhậu hấp dẫn khó quên.",
                            Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/202404240940237304.webp",
                            Name = "Gà H’Mong rang muối",
                            Price = 300000.0,
                            Type = "Suất"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsShow")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Infrastructure.Entities.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Icon")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsShow")
                        .HasColumnType("bit");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Url")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("Infrastructure.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deposit")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Discount")
                        .HasColumnType("float");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfCustomer")
                        .HasColumnType("int");

                    b.Property<double>("Payment")
                        .HasColumnType("float");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceTotal")
                        .HasColumnType("float");

                    b.Property<int>("RestaurantID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TableID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("VAT")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DishId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfCustomer")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.OrderTable", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("TableID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId", "TableID");

                    b.HasIndex("TableID");

                    b.ToTable("OrderTables", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.Restaurant", b =>
                {
                    b.Property<int>("RestaurantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestaurantID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RestaurantID");

                    b.ToTable("Restaurants", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.RoleOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccess")
                        .HasColumnType("bit");

                    b.Property<int>("OperationId")
                        .HasColumnType("int");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("RoleOperations");
                });

            modelBuilder.Entity("Infrastructure.Entities.Table", b =>
                {
                    b.Property<int>("TableID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TableID"), 1L, 1);

                    b.Property<int>("AreaID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumberOfDesk")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TableNumber")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TableID");

                    b.HasIndex("AreaID");

                    b.ToTable("Tables", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.UserOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccess")
                        .HasColumnType("bit");

                    b.Property<long>("OperationId")
                        .HasColumnType("bigint");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserOperations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.Area", b =>
                {
                    b.HasOne("Infrastructure.Entities.Restaurant", "Restaurant")
                        .WithMany("Areas")
                        .HasForeignKey("RestaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Infrastructure.Entities.Dish", b =>
                {
                    b.HasOne("Infrastructure.Entities.Category", "Category")
                        .WithMany("Dishes")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Infrastructure.Entities.OrderDetail", b =>
                {
                    b.HasOne("Infrastructure.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Infrastructure.Entities.OrderTable", b =>
                {
                    b.HasOne("Infrastructure.Entities.Order", "Order")
                        .WithMany("OrderTables")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.Table", "Table")
                        .WithMany("OrderTables")
                        .HasForeignKey("TableID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Infrastructure.Entities.Table", b =>
                {
                    b.HasOne("Infrastructure.Entities.Area", "Area")
                        .WithMany("Tables")
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Infrastructure.Entities.Area", b =>
                {
                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Infrastructure.Entities.Category", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("Infrastructure.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("OrderTables");
                });

            modelBuilder.Entity("Infrastructure.Entities.Restaurant", b =>
                {
                    b.Navigation("Areas");
                });

            modelBuilder.Entity("Infrastructure.Entities.Table", b =>
                {
                    b.Navigation("OrderTables");
                });
#pragma warning restore 612, 618
        }
    }
}
