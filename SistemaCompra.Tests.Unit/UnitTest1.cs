using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Collections.Generic;
using Xunit;

namespace SistemaCompra.Tests.Unit
{
    public class SolicitacaoCompraTests
    {
        [Fact]
        public void RegistrarCompra_Deve_DefinirCondicaoPagamento30_Quando_TotalMaiorQue50000()
        {
            var solicitacao = new SolicitacaoCompra("Cliente Teste", "Fornecedor Teste");
            var produto = new Produto("Produto A", "Descricao A", "Madeira", 60000m);

            solicitacao.RegistrarCompra(new List<Item>
            {
                new Item(produto, 1)
            });

            Assert.Equal(60000m, solicitacao.TotalGeral.Value);
            Assert.Equal(30, solicitacao.CondicaoPagamento.Valor);
        }

        [Fact]
        public void RegistrarCompra_Deve_LancarExcecao_Quando_TotalDeItensMenorOuIgualAZero()
        {
            var solicitacao = new SolicitacaoCompra("Cliente Teste", "Fornecedor Teste");
            var produto = new Produto("Produto B", "Descricao B", "Madeira", 100m);

            Assert.Throws<BusinessRuleException>(() =>
                solicitacao.RegistrarCompra(new List<Item>
                {
                    new Item(produto, 0)
                }));
        }
    }
}
