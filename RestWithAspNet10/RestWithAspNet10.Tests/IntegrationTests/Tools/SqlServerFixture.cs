using RestWithAspNet10.Configurations;
using Serilog;
using Testcontainers.MsSql;



namespace RestWithAspNet10.Tests.IntegrationTests.Tools
{
    public class SqlServerFixture : IAsyncLifetime
    {
        public MsSqlContainer Container { get; }

        public string ConnectionString => Container.GetConnectionString();

        public SqlServerFixture()
        {
            Container = new MsSqlBuilder().WithPassword("Test@1234").WithPortBinding(0,1433).Build();
        }
        public async ValueTask InitializeAsync()
        {      
            await Container.StartAsync();            
            EvolveConfig.ExecuteMigrations(ConnectionString);
        }

        public async ValueTask DisposeAsync()
        {
            await Container.DisposeAsync();
        }

    }
}
