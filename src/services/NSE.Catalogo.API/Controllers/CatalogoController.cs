using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Identidade;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Controllers
{
    [Authorize]
    public class CatalogoController : MainController
    {
        private readonly IProdutoRepository _repository;

        public CatalogoController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet("catalogo/produtos")]
        public async Task<IEnumerable<Produto>> Index()
        {
            return await _repository.ObterTodos();
        }

        [ClaimsAuthorize("Catalogo","Ler")]
        [HttpGet("catalogo/produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await _repository.ObterPorId(id);
        }
    }
}
