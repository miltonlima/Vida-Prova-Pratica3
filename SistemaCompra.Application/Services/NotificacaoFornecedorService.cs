using System;
using System.Threading.Tasks;

namespace SistemaCompra.Application.Services
{
    public class NotificacaoFornecedorService : INotificacaoFornecedorService
    {
        public Task NotificarFornecedorAsync(string fornecedor, string mensagem)
        {
            Console.WriteLine($"[NOTIFICAÇÃO FORNECEDOR] Fornecedor: {fornecedor} | Mensagem: {mensagem}");
            return Task.CompletedTask;
        }
    }
}
