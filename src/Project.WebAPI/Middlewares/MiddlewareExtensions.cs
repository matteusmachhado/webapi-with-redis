using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project.WebAPI.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder RunMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();

                // Aplicar as migrações
                dbContext.Database.Migrate();

                var random = new Random();
                var products = Enumerable.Range(1, 100)
                    .Select((number, index) => new Product($"Produto {number}", $"Descrição do produto: {number}", Math.Round((decimal)(random.NextDouble() * 100), 2)));

                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }

            return app;
        }
    }
}
