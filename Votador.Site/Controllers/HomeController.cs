using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;
using Votador.Site.Models;

namespace Votador.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string mensagem)
        {
            HttpContext.Session.TryGetValue("usuario.id", out byte[] usuarioIdBytes);

            if (usuarioIdBytes == null && mensagem == null)
                mensagem = "Faça login e comece logo a votar!";
            
            ViewData["Mensagem"] = mensagem;

            return View();
        }

        public IActionResult Privacidade()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
