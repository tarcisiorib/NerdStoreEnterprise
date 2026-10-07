using NSE.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLogin> Register(UsuarioRegistro usuarioRegistro);
        Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);
    }
}
