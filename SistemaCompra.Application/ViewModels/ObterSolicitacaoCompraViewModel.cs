using System;
using System.Collections.Generic;

namespace SistemaCompra.Application.ViewModels
{
    public class ObterSolicitacaoCompraViewModel
    {
        public Guid Id { get; set; }
        public string UsuarioSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public DateTime Data { get; set; }
        public decimal TotalGeral { get; set; }
        public string Situacao { get; set; }
        public IList<ObterItemSolicitacaoCompraViewModel> Itens { get; set; }
    }
}
