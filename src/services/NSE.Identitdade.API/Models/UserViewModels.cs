using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NSE.Identitdade.API.Models
{
    public class UsuarioRegistro
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }
        [Compare("Senha")]
        public string SenhaConfirmacao { get; set; }
    }
    public class UsuarioLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }
    }

    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }

    public class UsuarioToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }
    }

    public class UsuarioClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
