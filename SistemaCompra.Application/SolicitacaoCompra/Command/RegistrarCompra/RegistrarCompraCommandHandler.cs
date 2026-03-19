using MediatR;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : IRequestHandler<RegistrarCompraCommand>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly IProdutoRepository _produtoRepository;

        public RegistrarCompraCommandHandler(
            ISolicitacaoCompraRepository solicitacaoCompraRepository,
            IProdutoRepository produtoRepository)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _produtoRepository = produtoRepository;
        }

        async Task<Unit> IRequestHandler<RegistrarCompraCommand, Unit>.Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = new Domain.SolicitacaoCompraAggregate.SolicitacaoCompra(
                request.UsuarioSolicitante,
                request.NomeFornecedor);

            if (request.Itens != null)
            {
                foreach (var item in request.Itens)
                {
                    var produto = await _produtoRepository.ObterAsync(item.ProdutoId);
                    if (produto != null)
                    {
                        solicitacaoCompra.AdicionarItem(produto, item.Qtde);
                    }
                }
            }

            solicitacaoCompra.RegistrarCompra(solicitacaoCompra.Itens);
            await _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

            return Unit.Value;
        }
    }
}
