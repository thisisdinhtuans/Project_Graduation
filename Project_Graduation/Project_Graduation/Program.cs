using System.Text;
using System.Text.Json.Serialization;
using API.Data;
using Domain.Features;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AreaRepository;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BlogRepository;
using Infrastructure.Repositories.CategoryRepository;
using Infrastructure.Repositories.DishRepository;
using Infrastructure.Repositories.OrderDetailRepository;
using Infrastructure.Repositories.OrderRepository;
using Infrastructure.Repositories.TableRepository;
using Infrastructure.Services;
using Infrastructure.Services.AreaService;
using Infrastructure.Services.BlogService;
using Infrastructure.Services.CategoryService;
using Infrastructure.Services.DishService;
using Infrastructure.Services.OrderDetailService;
using Infrastructure.Services.OrderService;
using Infrastructure.Services.RestaurantService;
using Infrastructure.Services.RoleService;
using Infrastructure.Services.StaffService;
using Infrastructure.Services.StatisticService;
using Infrastructure.Services.TableService;
using Infrastructure.Services.UserService;
using Library.Extensions.Middleware;
using Library.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project_Graduation.Controllers;
using Project_Graduation.Lip;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // ??m b?o cookie ch? g?i qua HTTPS
    options.Cookie.SameSite = SameSiteMode.None;
});
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c=>
{
    var jwtSecurityScheme=new OpenApiSecurityScheme
    {
        BearerFormat="JWT",
        Name="Authorization",
        In=ParameterLocation.Header,
        Type=SecuritySchemeType.ApiKey,
        Scheme=JwtBearerDefaults.AuthenticationScheme,
        Description="Put Bearer + your token in the Stack below",
        Reference = new OpenApiReference
        {
            Id=JwtBearerDefaults.AuthenticationScheme,
            Type=ReferenceType.SecurityScheme,
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<Project_Graduation_Context>(opt=>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped(typeof(IAuditRepository<>), typeof(AuditRepository<>));

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IAreaService, AreaService>();

builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ITableService, TableService>();

builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IDishService, DishService>();

builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IStaffService, StaffService>();


builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IStaffService, StaffService>();

builder.Services.AddScoped<IStatisticService, StatisticService>();


builder.Services.AddCors();

builder.Services.AddIdentityCore<AppUser>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<Project_Graduation_Context>()
    .AddDefaultTokenProviders(); ;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt=>
    {
        opt.TokenValidationParameters=new TokenValidationParameters 
        {
            ValidateIssuer=false,
            ValidateAudience=false,
            ValidateLifetime=true,
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:TokenKey"]))
        };
    }
);
// builder.Services.AddReponsitories();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<PayLib>();
builder.Services.AddScoped<Util>();
builder.Services.AddScoped<PayCompare>();
builder.Services.AddScoped<VnPayController>();


builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("https://gocque.vercel.app", "http://localhost:3000", "https://nhahanggocque.vercel.app", "https://gocquerestaurant.vercel.app")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

//builder.Services.AddControllersWithViews()
//                .AddNewtonsoftJson(options =>
//                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//            );
var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSession();
//app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline

//app.UseHttpsRedirection(); // Đặt trước UseRouting và UseAuthorization
//app.UseRouting();
//app.UseCors("CorsPolicy");
//app.UseAuthentication();
//app.UseAuthorization();

//app.UseSession(); // Đặt trước UseRouting
//app.UseAuthentication();
//app.UseAuthorization();
//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseCors("CorsPolicy");
//app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseSession(); // Ensure session middleware is before routing and authentication
app.UseRouting(); // UseRouting should come before UseAuthentication and UseAuthorization
app.UseAuthentication(); // Required for JWT-based authentication
app.UseAuthorization();  // Required for authorization policies to work
app.UseCors("CorsPolicy"); // CORS should be placed correctly depending on the flow

app.UseMiddleware<ExceptionMiddleware>();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<Project_Graduation_Context>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    context.Database.Migrate();
    await DbInitializer.Initialize(context, userManager, roleManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "A problem occurred during migration");
}
app.MapControllers();
app.Run();
