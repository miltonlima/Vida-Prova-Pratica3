using System.Threading.Tasks;
using Testcontainers.MsSql;
using Xunit;

namespace SistemaCompra.Tests.Integration
{
    public class SqlServerContainerFixture : IAsyncLifetime
    {
        private MsSqlContainer? _container;
        public bool DockerDisponivel { get; private set; }

        public string ConnectionString
        {
            get
            {
                if (_container == null)
                {
                    return string.Empty;
                }

                return _container.GetConnectionString();
            }
        }

        public async Task InitializeAsync()
        {
            try
            {
                _container = new MsSqlBuilder()
                    .WithPassword("Your_strong_password123")
                    .Build();

                await _container.StartAsync();
                DockerDisponivel = true;
            }
            catch
            {
                DockerDisponivel = false;
            }
        }

        public async Task DisposeAsync()
        {
            if (_container != null)
            {
                await _container.DisposeAsync().AsTask();
            }
        }
    }
}
