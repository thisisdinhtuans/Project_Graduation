using Infrastructure.Configuration;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Project_Graduation_Context: IdentityDbContext<AppUser, AppRole, Guid>
    {
        public Project_Graduation_Context(DbContextOptions<Project_Graduation_Context> options) : base(options)
        {
        }
        public Project_Graduation_Context() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DishConfiguration());
            modelBuilder.ApplyConfiguration(new OrderTableConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());
            //Tạo bảng được cung cấp từ Identity
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
  

        modelBuilder.Entity<Category>().HasData(
                new Category { IdCategory = 1, Name = "HẢI SẢN", CreatedBy="Admin", CreatedDate=new DateTime(2023, 12, 25)},
                new Category { IdCategory = 2, Name = "ĐỒ NƯỚNG" , CreatedBy="Admin", CreatedDate=new DateTime(2023, 12, 25)},
                new Category { IdCategory = 3, Name = "CÁ CÁC MÓN", CreatedBy = "Admin", CreatedDate = new DateTime(2023, 12, 25) },
                new Category { IdCategory = 4, Name = "MÓN ĂN CHƠI", CreatedBy = "Admin", CreatedDate = new DateTime(2023, 12, 25) },
                new Category { IdCategory = 5, Name = "MÓN NHẬU", CreatedBy = "Admin", CreatedDate = new DateTime(2023, 12, 25) }
                );
modelBuilder.Entity<Dish>().HasData(
    new Dish
    {
        DishId = 1,
        Name = "Tôm chiên hoàng kim",
        Price = 225000,
        CategoryID = 1,
        Type = "Suất",
        Description = "Tên món: Tôm chiên hoàng kim\r\nĐịnh lượng: 1 suất 2-4 người ăn\r\nMô tả: Tôm chiên hoàng kim là một trong những món nhậu cực kỳ tốn bia tại Quán Nhậu Tự Do.\r\nTôm tươi, căng mọng đem chiên vàng giòn rồi lại đảo qua gia vị đậm đà tạo nên món ăn hấp dẫn, ăn một miếng là không ngừng lại được.",
        Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/20240424094950598.webp",
        CreatedBy = "Admin",
        CreatedDate = new DateTime(2023, 12, 25)
    },
    new Dish
    {
        DishId = 2,
        Name = "Tôm sú sốt ớt pattaya",
        Price = 225000,
        CategoryID = 1,
        Type = "Suất",
        Description = "Tên món: Tôm sú sốt ớt pattaya\r\nĐịnh lượng: 1 suất cho 3-4 người ăn\r\nMô tả: Tôm sú sốt ớt pattaya là một trong những món ăn mới lạ không thể bỏ qua khi ghé Quán Nhậu Tự Do.\r\nThịt tôm sú dai giòn ăn cùng rau củ, thấm đẫm nước sốt ớt pattaya “thần thánh\" xen lẫn vị chua chua, ngọt ngọt, cay nồng chắc chắn sẽ làm hài lòng mọi thực khách.",
        Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/06/202406051717238266.webp"
        ,
        CreatedBy = "Admin",
        CreatedDate = new DateTime(2023, 12, 25)
    },
    new Dish
    {
        DishId = 3,
        Name = "Lợn mán nướng mắc khén",
        Price = 185000,
        CategoryID = 2,
        Type = "Suất",
        Description = "Tên món: Lợn mán nướng mắc khén\r\nĐịnh lượng: 1 suất cho 3-4 người ăn\r\nMô tả: Lợn mán nướng mắc khén là một trong những món nhậu kích thích vị giác của thực khách.\r\nThịt lợn mềm ngọt, có chút dai dai, không quá mỡ được nướng cùng hạt mắc khén thơm lừng. Sự hòa trộn giữa vị đậm đà của thịt lợn mán cùng các loại rau củ ăn kèm mang đến cho món ăn hương vị tuyệt hảo không thể nào quên.\r\n",
        Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/202404231915283755.webp",
        CreatedBy = "Admin",
        CreatedDate = new DateTime(2023, 12, 25)
    },
    new Dish
    {
        DishId = 4,
        Name = "Gà đen nướng mắc khén",
        Price = 300000,
        CategoryID = 2,
        Type = "Suất",
        Description = "Tên món: Gà đen nướng mắc khén\r\nĐịnh lượng: Gà nửa con, hạt mắc khén rang thơm, quả ớt cay, lá chanh, muối,củ sả.\r\nMô tả: Chuẩn gia vị chẩm chéo để chấm gà, thơm phức mùi mắc kén và tê tê đầu lưỡi.",
        Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/05/20240530101506515.webp",
        CreatedBy = "Admin",
        CreatedDate = new DateTime(2023, 12, 25)
    },
    new Dish
    {
        DishId = 5,
        Name = "Gà H’Mong rang muối",
        Price = 300000,
        CategoryID = 5,
        Type = "Suất",
        Description = "Tên món: Gà H’Mong rang muối\r\nĐịnh lượng: 800g\r\nMô tả: Gà H’Mong rang muối là món ăn được nhiều anh em sành ăn yêu thích tại Quán Nhậu Tự Do.\r\nGà H’Mong được tuyển chọn từ giống gà đen nuôi tự nhiên nên thịt rất săn chắc và thơm ngon. Đem gà chiên giòn và xóc đều với bột muối, sả, hành và lá chanh là đủ để có một món nhậu hấp dẫn khó quên.",
        Image = "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/202404240940237304.webp",
        CreatedBy = "Admin",
        CreatedDate = new DateTime(2023, 12, 25)
    }
    );
}
public DbSet<Area> Areas { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<OrderTable> OrderTables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserOperation> UserOperations { get; set; }
        public DbSet<RoleOperation> RoleOperations { get; set; }
    }
}