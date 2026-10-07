using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Controllers
{
    public class ClienteController : MainController
    {
        private readonly IMediatorHandler _mediator;

        public ClienteController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            var resultado = await _mediator.EnviarComando(
                new RegistrarClienteCommand(Guid.NewGuid(), "Tarcísio", "tarcisio@teste.com", "86508253204"));

            return CustomResponse(resultado);
        }
    }
}
