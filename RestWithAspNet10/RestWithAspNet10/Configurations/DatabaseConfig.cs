using Microsoft.EntityFrameworkCore;
using RestWithAspNet10.Model.Context;

namespace RestWithAspNet10.Configurations
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["MSSQLServerSQLConnection:MSSQLServerSQLConnectionStrings"];

            if(string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("Connection string 'MSSQLServerSQLConnectionString' not found");
            }

            services.AddDbContext<MSSQLContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}
