using System.Threading.Tasks;
using Testcontainers.MsSql;
using Xunit;

namespace SistemaCompra.Tests.Integration
{
    public class SqlServerContainerFixture : IAsyncLifetime
    {
        private readonly MsSqlContainer _container;

        public SqlServerContainerFixture()
        {
            _container = new MsSqlBuilder()
                .WithPassword("Your_strong_password123")
                .Build();
        }

        public string ConnectionString => _container.GetConnectionString();

        public Task InitializeAsync()
        {
            return _container.StartAsync();
        }

        public Task DisposeAsync()
        {
            return _container.DisposeAsync().AsTask();
        }
    }
}
