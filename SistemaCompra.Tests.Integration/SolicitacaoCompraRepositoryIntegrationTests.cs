using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data;
using SistemaCompra.Infra.Data.SolicitacaoCompra;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SistemaCompra.Tests.Integration
{
    public class SolicitacaoCompraRepositoryIntegrationTests : IClassFixture<SqlServerContainerFixture>
    {
        private readonly SqlServerContainerFixture _fixture;

        public SolicitacaoCompraRepositoryIntegrationTests(SqlServerContainerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task RegistrarCompra_E_ObterAsync_Devem_Persistir_E_CarregarDados()
        {
            var dbName = $"SistemaCompraTest_{Guid.NewGuid():N}";
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_fixture.ConnectionString)
            {
                InitialCatalog = dbName,
            };
            var connectionString = sqlConnectionStringBuilder.ConnectionString;

            var options = new DbContextOptionsBuilder<SistemaCompraContext>()
                .UseSqlServer(connectionString)
                .Options;

            Guid solicitacaoId;

            using (var context = new SistemaCompraContext(options))
            {
                await context.Database.EnsureCreatedAsync();

                var produto = new Produto("Produto Integracao", "Descricao Integracao", "Madeira", 1500m);
                await context.Set<Produto>().AddAsync(produto);
                await context.SaveChangesAsync();

                var solicitacao = new SolicitacaoCompra("Cliente Integracao", "Fornecedor Integracao");
                solicitacao.RegistrarCompra(new List<Item>
                {
                    new Item(produto, 2)
                });

                var repository = new SolicitacaoCompraRepository(context);
                await repository.RegistrarCompra(solicitacao);
                await context.SaveChangesAsync();

                solicitacaoId = solicitacao.Id;
            }

            using (var context = new SistemaCompraContext(options))
            {
                var repository = new SolicitacaoCompraRepository(context);
                var resultado = await repository.ObterAsync(solicitacaoId);

                Assert.NotNull(resultado);
                Assert.NotNull(resultado.Itens);
                Assert.Single(resultado.Itens);
                Assert.Equal(3000m, resultado.TotalGeral.Value);
                Assert.Equal(0, resultado.CondicaoPagamento.Valor);
                Assert.Equal("Produto Integracao", resultado.Itens[0].Produto.Nome);
            }
        }
    }
}
