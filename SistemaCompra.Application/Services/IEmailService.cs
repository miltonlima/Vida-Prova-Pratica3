using System.Threading.Tasks;

namespace SistemaCompra.Application.Services
{
    public interface IEmailService
    {
        Task EnviarParaClienteAsync(string cliente, string assunto, string mensagem);
    }
}
