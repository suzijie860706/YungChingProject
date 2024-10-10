using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using YungChingProject.Data;
using YungChingProject.Models;

var builder = WebApplication.CreateBuilder(args);

// 註冊 DbContext 並使用 SQLite
builder.Services.AddDbContext<NorthwindContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("NorthwindConnection")));

builder.Services.AddControllers();

// 註冊 Swagger
builder.Services.AddSwaggerGen();

#region swagger
builder.Services.AddSwaggerGen(options =>
{
    //表頭描述
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "YungChingProject",
        Description = $@"
An ASP.NET Core 6 Web API for YungChingProject.  
Some useful links:
- [The YungChingProject repository](https://github.com/suzijie860706/YungChingProject/tree/develop)",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    //xml文件增加註解
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
#endregion

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
