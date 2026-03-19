using System.Threading.Tasks;

namespace SistemaCompra.Application.Services
{
    public interface INotificacaoFornecedorService
    {
        Task NotificarFornecedorAsync(string fornecedor, string mensagem);
    }
}
