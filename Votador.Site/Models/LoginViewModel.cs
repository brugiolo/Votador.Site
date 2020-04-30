using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Votador.Site.Models
{
    public class LoginViewModel
    {
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
