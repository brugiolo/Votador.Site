using System.Net;

namespace Votador.Site.Models
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
    }

    public class LoginResultViewModel
    {
        public LoginViewModel Login { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
