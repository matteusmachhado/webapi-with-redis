using Project.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Project.WebAPI.Middlewares;
using Project.Domain.Interfaces.Repositories;
using Project.Persistence.Repositories;
using Project.Application.Interfaces;
using Project.Application.Services;
using Project.Application.Features;
using Project.Domain.Interfaces;
using Project.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Sql Server
builder.Services.AddDbContext<ProjectDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Cache Redis
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    redisOptions.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositorios
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Serviços
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IValidatorService, ValidatorService>();

// MediatR
builder.Services.AddMediatR(x =>
{
    x.Lifetime = ServiceLifetime.Scoped;
    x.RegisterServicesFromAssemblies(typeof(CommandHandler).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RunMigrations();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
