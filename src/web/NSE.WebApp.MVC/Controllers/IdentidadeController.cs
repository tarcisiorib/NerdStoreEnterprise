using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentidadeController : MainController
    {
        private readonly IAutenticacaoService _authService;

        public IdentidadeController(IAutenticacaoService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("registrar")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("registrar")]
        public async Task<ActionResult> Register(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return View(usuarioRegistro);

            var result = await _authService.Register(usuarioRegistro);

           if (ResponsePossuiErros(result.ResponseResult)) return View(usuarioRegistro);

            await RealizarLogin(result);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("entrar")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("entrar")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid) return View(usuarioLogin);

            var result = await _authService.Login(usuarioLogin);

            if (ResponsePossuiErros(result.ResponseResult)) return View(usuarioLogin);

            await RealizarLogin(result);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("", "Catalogo");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("", "Catalogo");
        }

        private async Task RealizarLogin(UsuarioRespostaLogin usuarioRespostaLogin)
        {
            var token = ObterTokenFormatado(usuarioRespostaLogin.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", usuarioRespostaLogin.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
    }
}
