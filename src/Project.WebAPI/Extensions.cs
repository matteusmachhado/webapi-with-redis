using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Persistence.Contexts;

namespace Project.WebAPI
{
    public static class Extensions
    {
        public static IApplicationBuilder RunMigrations(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();

            try
            {
                var time = 30 * 1000;
                logger.LogInformation($"Aguardando {time}ms para executar as migrações.");
                Thread.Sleep(time);

                using (var dbScope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = dbScope.ServiceProvider.GetRequiredService<ProjectDbContext>();

                    logger.LogInformation("Executando migrações...");

                    // Aplicar as migrações
                    dbContext.Database.Migrate();

                    var random = new Random();
                    var products = Enumerable.Range(1, 100)
                        .Select((number, index) => new Product($"Produto {number}", $"Descrição do produto: {number}", Math.Round((decimal)(random.NextDouble() * 100), 2)));

                    dbContext.Products.AddRange(products);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao executar as migrações: {ex.Message}", ex);
            }

            return app;
        }
    }
}
