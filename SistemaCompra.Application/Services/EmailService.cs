using System;
using System.Threading.Tasks;

namespace SistemaCompra.Application.Services
{
    public class EmailService : IEmailService
    {
        public Task EnviarParaClienteAsync(string cliente, string assunto, string mensagem)
        {
            Console.WriteLine($"[EMAIL] Destinatário: {cliente} | Assunto: {assunto} | Mensagem: {mensagem}");
            return Task.CompletedTask;
        }
    }
}
