using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YungChingProject.Data;
using YungChingProject.Models;

var builder = WebApplication.CreateBuilder(args);

// 註冊 DbContext 並使用 SQLite
builder.Services.AddDbContext<NorthwindContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("NorthwindConnection")));

builder.Services.AddControllers();

// 註冊 Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

//讀取SeedData
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        // 指定路徑為 ""
        c.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
