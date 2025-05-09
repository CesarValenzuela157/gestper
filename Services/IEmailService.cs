// Services/IEmailService.cs
using System.Threading.Tasks;

namespace Gestper.Services
{
    public interface IEmailService
    {
        Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo);
    }
}