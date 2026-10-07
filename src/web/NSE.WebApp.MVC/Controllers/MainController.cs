using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using System.Linq;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors.Mensagens.Any())
            {
                foreach (var menssagem in resposta.Errors.Mensagens)
                    ModelState.AddModelError(string.Empty, menssagem);

                return true;
            }

            return false;
        }
    }
}
