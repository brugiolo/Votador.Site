using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using Votador.Site.Models;
using Votador.Site.Service;

namespace Votador.Site.Controllers
{
    public class LoginController : Controller
    {
        //[AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Autenticar(LoginViewModel login)
        {
            var loginService = new LoginService();
            var resultado = await loginService.Autenticar(login);

            if (resultado.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContext.Session.Set("token", Encoding.ASCII.GetBytes(resultado.Login.Token));
                HttpContext.Session.Set("usuario.email", Encoding.ASCII.GetBytes(resultado.Login.Email));
                HttpContext.Session.Set("usuario.id", Encoding.ASCII.GetBytes(resultado.Login.Id.ToString()));

                return RedirectToAction("Apresentacao", "Recurso");
            }
            else
            {
                return RedirectToAction("Index", "Home", new { mensagem = "Usuário e/ou senha inválidos." });
            }
        }

        [HttpPost]
        public ActionResult Logout(LoginViewModel login)
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("usuario.email");
            HttpContext.Session.Remove("usuario.id");

            return RedirectToAction("Index", "Home");
        }
    }
}