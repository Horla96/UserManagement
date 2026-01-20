using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserManagement_API.Data;
using UserManagement_API.Interfaces;
using UserManagement_API.Repositories;
using UserManagement_API.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); // for [SwaggerOperation]
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User Management API",
        Version = "v1" // this fixes your version error
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
