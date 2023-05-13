using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Options;
using TimeTracker.DAL.Factories;
using TimeTracker.DAL;
using TimeTracker.DAL.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TimeTracker.App;


public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        DALOptions dalOptions = new();
        configuration.GetSection("TimeTracker:DAL").Bind(dalOptions);

        services.AddSingleton<DALOptions>(dalOptions);

        if (dalOptions.Sqlite is null)
        {
            throw new InvalidOperationException("No persistence provider configured");
        }

        if (dalOptions.Sqlite?.Enabled == false)
        {
            throw new InvalidOperationException("No persistence provider enabled");
        }

        if (dalOptions.Sqlite?.Enabled == true)
        {
            if (dalOptions.Sqlite.DatabaseName is null)
            {
                throw new InvalidOperationException($"{nameof(dalOptions.Sqlite.DatabaseName)} is not set");

            }
            string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, dalOptions.Sqlite.DatabaseName!);
            services.AddSingleton<IDbContextFactory<TimeTrackerDbContext>>(provider => new DbContextSqLiteFactory(databaseFilePath, dalOptions?.Sqlite?.SeedDemoData ?? false));
            services.AddSingleton<IDbMigrator, SqliteDbMigrator>();
        }

        services.AddSingleton<ActivityEntityMapper>();
        services.AddSingleton<ProjectAmountEntityMapper>();
        services.AddSingleton<ProjectEntityMapper>();
        services.AddSingleton<UserEntityMapper>();

        return services;
    }
}

