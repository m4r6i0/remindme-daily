using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RemindMeDaily.API.Data;
using RemindMeDaily.API.Repositories;
using RemindMeDaily.Domain.Interfaces;
using RemindMeDaily.API.Services.Interfaces;
using RemindMeDaily.API.Services;

namespace RemindMeDaily.API.Configurations;

public static class ServiceConfiguration
{

    /// <summary>
    /// Configura as dependências da aplicação.
    /// </summary>
    /// <param name="services">O IServiceCollection para registrar dependências.</param>
    /// <param name="configuration">A configuração do aplicativo.</param>
    /// <param name="DbContextOptionsBuilder">Configurador de contextos.</param>
    public static void ConfigureDependencies(this IServiceCollection services, 
                                            IConfiguration configuration,
                                            Action<DbContextOptionsBuilder> dbContextOptions = null)
    {

        ConfigureDatabaseOptions(services, configuration, dbContextOptions);

        // Registro do repositório genérico
        services.AddScoped<IReminderRepository, ReminderRepository>();

        // Registro do serviço
        services.AddScoped<IReminderService, ReminderService>();

    }

    /// <summary>
    /// Configura bases de dados local para testes/prod.
    /// </summary>
    /// <param name="services">O IServiceCollection para registrar dependências.</param>
    /// <param name="configuration">A configuração do aplicativo.</param>
    /// <param name="DbContextOptionsBuilder">Configurador de contextos.</param>
    static void ConfigureDatabaseOptions(IServiceCollection services, 
                                         IConfiguration configuration, 
                                         Action<DbContextOptionsBuilder> dbContextOptions)
    {
        // Verifica se uma configuração de DbContext foi fornecida
        if (dbContextOptions == null)
        {
            var dbConfig = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();

            //local do database
            var databaseLocale = $"{dbConfig.DatabaseFolder}/{dbConfig.DatabaseName}";

            // Configuração padrão para produção com SQLite
            var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseLocale);

            // Criação do diretório se necessário
            var directory = Path.GetDirectoryName(databasePath);
            CheckDatabaseDirectory(directory);

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={databasePath}"));
        }
        else
        {
            // Usa a configuração fornecida (geralmente em testes)
            services.AddDbContext<AppDbContext>(dbContextOptions);
        }
    }

    static void CheckDatabaseDirectory(string? directory)
    {
        if (string.IsNullOrEmpty(directory)) return;

        try
        {
            // Cria o diretório se não existir
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                return;
            }

            // Limpa o conteúdo do diretório
            foreach (var file in Directory.GetFiles(directory))
            {
                try
                {
                    File.Delete(file);
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed database in Directory: {directory}. Details: {ex.Message}");
        }
    }

}
