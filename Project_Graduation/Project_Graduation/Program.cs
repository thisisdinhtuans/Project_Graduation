using System.Text;
using API.Data;
using Domain.Features;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Services;
using Library.Extensions.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
builder.Services.AddCors();

builder.Services.AddIdentityCore<AppUser>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<Project_Graduation_Context>();

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
builder.Services.AddScoped<TokenService>();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt=>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
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
app.Run();
