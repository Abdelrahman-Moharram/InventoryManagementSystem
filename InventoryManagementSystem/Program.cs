using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ______________________________ Sql Confs_________________________________//

builder.Services.AddDbContext<ApplicationDbContext>(
    options=>
        options.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection") )
    );
// ______________________________ End Sql Conf_________________________________//






// ______________________________ Identity Confs _________________________________//

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
// ______________________________ End Sql Conf_________________________________//

var app = builder.Build();








// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
