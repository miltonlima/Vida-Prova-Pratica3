using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }
        public Situacao Situacao { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Itens = new List<Item>();
            Data = DateTime.Now;
            TotalGeral = new Money();
            CondicaoPagamento = new CondicaoPagamento(0);
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            if (Itens == null) Itens = new List<Item>();
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            if (itens == null) throw new ArgumentNullException(nameof(itens));

            Itens = itens.ToList();
            if (Itens.Sum(c => c.Qtde) <= 0) throw new BusinessRuleException("Total de itens de compra deve ser maior que zero.");

            TotalGeral = new Money(Itens.Sum(c => c.Subtotal.Value));
            CondicaoPagamento = DefinirCondicaoPagamento();
        }

        private CondicaoPagamento DefinirCondicaoPagamento()
        {
            if (TotalGeral.Value > 50000)
            {
                return new CondicaoPagamento(30);
            }

            return new CondicaoPagamento(0);
        }
    }
}
