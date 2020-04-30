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

                return RedirectToAction("Apresentacao", "Recurso");
            }
            else
            {
                ModelState.AddModelError("", "Usuário e/ou senha inválidos.");
                return View();
            }
        }
    }
}