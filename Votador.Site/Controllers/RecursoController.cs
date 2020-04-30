using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Votador.Site.Models;
using Votador.Site.Service;

namespace Votador.Site.Controllers
{
    public class RecursoController : Controller
    {
        public static string BaseUri = "https://localhost:44364/";
        List<RecursoViewModel> recursos = new List<RecursoViewModel>();

        public async Task<IActionResult> Apresentacao()
        {
            var recursos = new List<RecursoViewModel>();
            var tokenValido = HttpContext.Session.TryGetValue("token", out byte[] tokenBytes);

            if (tokenValido)
            {
                var recursoService = new RecursoService();
                var token = Encoding.Default.GetString(tokenBytes);

                recursos = await recursoService.ObterApresentacao(token);
            }

            return View(recursos);
        }

        public async Task<IActionResult> Resultado()
        {
            var recursos = new List<RecursoVotosViewModel>();
            var tokenValido = HttpContext.Session.TryGetValue("token", out byte[] tokenBytes);

            if (tokenValido)
            {
                var recursoService = new RecursoService();
                var token = Encoding.Default.GetString(tokenBytes);

                recursos = await recursoService.ObterResultado(token);
            }

            return View(recursos);
        }
    }
}