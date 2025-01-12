using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain;
using TaskManagementApp.Persistance;
using TaskManagementApp.Persistance.Context;
using TaskManagementApp.Persistance.Repositories;
using TaskManagementApp.Persistance.Services;
using TaskManagementApp.Persistance.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserTaskService, UserTaskService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlServer")));

builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPİ", Version = "V1" });

});
builder.Services.AddCors(options =>
     {
         options.AddPolicy("AllowAll", builder =>
             builder.AllowAnyOrigin() // Herhangi bir kaynağa izin ver
                    .AllowAnyMethod()  // Tüm HTTP metodlarına izin ver
                    .AllowAnyHeader()); // Tüm başlıklara izin ver
     });


var app = builder.Build();
app.MapControllers();

app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));

}

app.UseHttpsRedirection();



app.Run();


