using BookStore.Seeds;
using InventoryManagementSystem.Controllers;
using InventoryManagementSystem.Domain.Helpers;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Data;
using InventoryManagementSystem.Infrastructure.Filters;
using InventoryManagementSystem.Infrastructure.Mappers;
using InventoryManagementSystem.Infrastructure.Repositories;
using InventoryManagementSystem.Infrastructure.Seeds;
using InventoryManagementSystem.Infrastructure.Services.AuthServices;
using InventoryManagementSystem.Infrastructure.Services.BrandServices;
using InventoryManagementSystem.Infrastructure.Services.CategoryServices;
using InventoryManagementSystem.Infrastructure.Services.InventoryServices;
using InventoryManagementSystem.Infrastructure.Services.OrderServices;
using InventoryManagementSystem.Infrastructure.Services.Productservices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ______________________________ Sql Confs_________________________________//

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
// ______________________________ End Sql Conf_________________________________//


// ______________________________ Dependancy Injections _________________________________//
    builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IRoleService, RoleService>();

    builder.Services.AddScoped<IProductService, Productservice>();
    builder.Services.AddScoped<IBrandService, BrandService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IInventoryService, InventoryService>();
    builder.Services.AddScoped<IOrderService, OrderService>();


// Permissions
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
    builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
// ______________________________ End Dependancy Injections _________________________________//



// ______________________________ Identity Confs _________________________________//


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options =>
    {
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    }
    ).AddEntityFrameworkStores<ApplicationDbContext>();


// ------------------------------------------------------- //

// ------------------------- JwtBearer Conf ----------------------//

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(op =>
    {
        op.RequireHttpsMetadata = true;
        op.SaveToken = false;
        op.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,

            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SECRETKEY"]))
        };
    });

// ------------------------------------------------------- //

// ------------------------- Other Confs ----------------------//

// Logger
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Logging.AddSerilog(logger);

// auto mapper
builder.Services.AddAutoMapper(typeof(BrandProfile));
builder.Services.AddAutoMapper(typeof(CategoryProfile));

builder.Services.AddAutoMapper(typeof(InventoryProfile));
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddAutoMapper(typeof(ProductItemProfile));
builder.Services.AddAutoMapper(typeof(ProductsInventoryProfile));
builder.Services.AddAutoMapper(typeof(OrderProfile));


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                          policy.AllowAnyOrigin();
                      });
});

// ------------------------------------------------------- //




var app = builder.Build();






// ---------------------------  Data Seeding    ---------------------------- //
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerAccounts = services.GetRequiredService<ILogger<AccountsController>>();

var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

var authService = services.GetRequiredService<IAuthService>();
var roleService = services.GetRequiredService<IRoleService>();




try
{
    if (!roleManager.Roles.Any())
    {

        await DefaultRoles.SeedAsync(roleService);
        
        await DefaultUsers.SeedCustomerAsync(authService, roleService, roleManager);
        await DefaultUsers.SeedSupplierAsync(authService, roleService, roleManager);
        await DefaultUsers.SeedAdminAsync(authService, roleService, roleManager);
        await DefaultUsers.SeedSuperAdminAsync(authService, roleService, roleManager);

    }
}
catch (Exception ex)
{
    // todo: add to logger
    loggerAccounts.LogError(ex, "an Error ocurred while seeding initial data");
}

// ------------------------------------------------------- //



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
