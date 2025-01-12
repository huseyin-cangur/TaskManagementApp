using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain;
using TaskManagementApp.Domain.Entities;
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
using (var scoped = app.Services.CreateScope())
{
     var services = scoped.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate(); // Veritabanını otomatik güncelle

            SeedData.Initialize(services); 
}


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

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();

        // Veritabanı boşsa veriyi ekle
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User
                {
                    Id = "8fb3566d-ce2e-45bf-8186-5b417c6c5a1",
                    FullName = "Hüseyin",
                    IdentityNumber = "25214245414",
                    Password = "1234",
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime()
                },
                  new User
                  {
                      Id = "e4faad77-28cf-45f6-905a-379b3d193923",
                      FullName = "Ahmet",
                      IdentityNumber = "35254452142",
                      Password = "4321",
                      CreatedDate = DateTime.Now.ToUniversalTime(),
                      UpdatedDate = DateTime.Now.ToUniversalTime()
                  },
                 new User
                 {
                     Id = "6f4de15f-0769-4c64-9888-63035cd78046",
                     FullName = "Selin",
                     IdentityNumber = "325425412452",
                     Password = "22222",
                     CreatedDate = DateTime.Now.ToUniversalTime(),
                     UpdatedDate = DateTime.Now.ToUniversalTime()
                 }

            );

        }
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role
                {
                    Id = "e09a4803-eac3-4fe0-a48b-052d7cf83bd9",
                    Name = "Admin",
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime()
                },

                 new Role
                 {
                     Id = "22348bf9-2f2b-4a82-8709-ced051953f7f",
                     Name = "User",
                     CreatedDate = DateTime.Now.ToUniversalTime(),
                     UpdatedDate = DateTime.Now.ToUniversalTime()
                 }
            );
        }
        if (!context.UserRoles.Any())


        {
            context.UserRoles.AddRange(
                new UserRole
                {
                    Id = "aa2201b7-ea70-42e7-95f5-1116ab710f99",
                    UserId = "8fb3566d-ce2e-45bf-8186-5b417c6c5a1",
                    RoleId = "e09a4803-eac3-4fe0-a48b-052d7cf83bd9",
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime()
                },
                 new UserRole
                {
                    Id = "1790bb85-c578-4198-acd9-23a2088f2dd7",
                    UserId = "e4faad77-28cf-45f6-905a-379b3d193923",
                    RoleId = "22348bf9-2f2b-4a82-8709-ced051953f7f",
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime()
                },
                new UserRole
                {
                    Id = "a2d0c1d2-39ef-4155-a82c-21c6c009f7ff",
                    UserId = "6f4de15f-0769-4c64-9888-63035cd78046",
                    RoleId = "22348bf9-2f2b-4a82-8709-ced051953f7f",
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime()
                }
            );
        }

       if(!context.Tasks.Any())
       {
            context.Tasks.AddRange(
                new TaskManagementApp.Domain.Entities.Task {
                     Id="aae4f1da-3421-4b14-88be-5fd84e2750e8",
                     Title="test task1",
                     Description="test task 1 desc",
                     Status=false,
                     CreatedDate=DateTime.Now.ToUniversalTime(),
                     UpdatedDate=DateTime.Now.ToUniversalTime()                   
                },
                new TaskManagementApp.Domain.Entities.Task {
                     Id="3f7c3e62-cdb6-48b5-88d7-ba501bd6805d",
                     Title="test task2",
                     Description="test task 2 desc",
                     Status=false,
                     CreatedDate=DateTime.Now.ToUniversalTime(),
                     UpdatedDate=DateTime.Now.ToUniversalTime()                   
                },
                new TaskManagementApp.Domain.Entities.Task {
                     Id="0188834f-138c-4ade-b779-2533afac9b15",
                     Title="test task3",
                     Description="test task 3 desc",
                     Status=false,
                     CreatedDate=DateTime.Now.ToUniversalTime(),
                     UpdatedDate=DateTime.Now.ToUniversalTime()                   
                }

            );
       }


       if(!context.UserTasks.Any())
       {
           context.UserTasks.AddRange(
             new UserTask {
                Id="864c7943-7a2b-47f9-8442-ce9f230f4bef",
                UserId="8fb3566d-ce2e-45bf-8186-5b417c6c5a1",
                TaskId="aae4f1da-3421-4b14-88be-5fd84e2750e8",
                CreatedDate=DateTime.Now.ToUniversalTime(),
                UpdatedDate=DateTime.Now.ToUniversalTime()
             },
               new UserTask {
                Id="bba102b4-a513-40bb-9518-ace2ae05e58f",
                UserId="e4faad77-28cf-45f6-905a-379b3d193923",
                TaskId="aae4f1da-3421-4b14-88be-5fd84e2750e8",
                CreatedDate=DateTime.Now.ToUniversalTime(),
                UpdatedDate=DateTime.Now.ToUniversalTime()
             },
               new UserTask {
                Id="caf8c816-e5e0-4000-a74d-a065f8206d18",
                UserId="e4faad77-28cf-45f6-905a-379b3d193923",
                TaskId="0188834f-138c-4ade-b779-2533afac9b15",
                CreatedDate=DateTime.Now.ToUniversalTime(),
                UpdatedDate=DateTime.Now.ToUniversalTime()
             },
               new UserTask {
                Id="56123e06-b83f-4925-b037-c9e58b1642e8",
                UserId="e4faad77-28cf-45f6-905a-379b3d193923",
                TaskId="0188834f-138c-4ade-b779-2533afac9b15",
                CreatedDate=DateTime.Now.ToUniversalTime(),
                UpdatedDate=DateTime.Now.ToUniversalTime()
             },
              new UserTask {
                Id="2fbf17b9-a400-4bfe-8cc2-b14ed2783c82",
                UserId="e4faad77-28cf-45f6-905a-379b3d193923",
                TaskId="0188834f-138c-4ade-b779-2533afac9b15",
                CreatedDate=DateTime.Now.ToUniversalTime(),
                UpdatedDate=DateTime.Now.ToUniversalTime()
             }
           );
       }

        context.SaveChanges();
    }
}
