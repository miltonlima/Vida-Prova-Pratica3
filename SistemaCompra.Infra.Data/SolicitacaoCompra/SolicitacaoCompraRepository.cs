using Microsoft.EntityFrameworkCore;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Threading.Tasks;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraRepository : SolicitacaoAgg.ISolicitacaoCompraRepository
    {
        private readonly SistemaCompraContext _context;

        public SolicitacaoCompraRepository(SistemaCompraContext context)
        {
            _context = context;
        }

        public async Task<SolicitacaoAgg.SolicitacaoCompra> ObterAsync(Guid id)
        {
            return await _context.Set<SolicitacaoAgg.SolicitacaoCompra>()
                .Include(c => c.Itens)
                .ThenInclude(c => c.Produto)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task RegistrarAsync(SolicitacaoAgg.SolicitacaoCompra solicitacaoCompra)
        {
            await _context.Set<SolicitacaoAgg.SolicitacaoCompra>().AddAsync(solicitacaoCompra);
        }
    }
}
