using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Maincipito.Persistence;

public class MaincipitoDbContextFactory : IDesignTimeDbContextFactory<MaincipitoDbContext>
{
    public MaincipitoDbContext CreateDbContext(string[] args)
    {
        // Resuelve la configuración desde UserSecrets, appsettings o Variables de Entorno sin hardcodear
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("MyAppContext")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__MyAppContext")
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'MyAppContext' en User Secrets ni en Variables de Entorno.");

        var optionsBuilder = new DbContextOptionsBuilder<MaincipitoDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new MaincipitoDbContext(optionsBuilder.Options);
    }
}