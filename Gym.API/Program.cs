using Gym.API.Middleware;
using Gym.Application;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Infrastructure.Data;
using Gym.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddApplication();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<GlobalExceptionMiddleware>();

builder.Services.AddOpenApi();

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters
        .Add(new JsonStringEnumConverter());
    });

builder.Services.AddSwaggerGen(o =>
{
    o.UseInlineDefinitionsForEnums();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
