using System;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarItemSolicitacaoCompraCommand
    {
        public Guid ProdutoId { get; set; }
        public int Qtde { get; set; }
    }
}
