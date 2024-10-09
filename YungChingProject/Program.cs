using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YungChingProject.Data;

var builder = WebApplication.CreateBuilder(args);

// µù¥U DbContext ¨Ã¨Ï¥Î SQLite
builder.Services.AddDbContext<NorthwindContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("NorthwindConnection")));

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
