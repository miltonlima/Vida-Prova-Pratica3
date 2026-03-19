using System;

namespace SistemaCompra.Application.ViewModels
{
    public class ObterItemSolicitacaoCompraViewModel
    {
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Qtde { get; set; }
        public decimal Subtotal { get; set; }
    }
}
