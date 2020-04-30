using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using Votador.Site.Models;
using Votador.Site.Service;
using System.Linq;

namespace Votador.Site.Controllers
{
    public class VotoController : Controller
    {
        public async Task<IActionResult> Votacao(int recursoId)
        {
            HttpContext.Session.TryGetValue("token", out byte[] tokenBytes);
            HttpContext.Session.TryGetValue("usuario.email", out byte[] emailBytes);

            var token = Encoding.Default.GetString(tokenBytes);
            var email = Encoding.Default.GetString(emailBytes);

            var recursoService = new RecursoService();
            var recursos = await recursoService.ObterApresentacao(token);
            var recurso = recursos.FirstOrDefault(r => r.Id == recursoId);

            var votoViewModel = new VotoViewModel
            {
                UsuarioEmail = email,
                Comentario = string.Empty,
                Recurso = recurso
            };

            return View(votoViewModel);
        }

        public async Task<IActionResult> ConfirmarVoto(VotoViewModel votoViewModel)
        {
            var tokenValido = HttpContext.Session.TryGetValue("token", out byte[] tokenBytes);
            if (tokenValido)
            {
                var votoService = new VotoService();
                var token = Encoding.Default.GetString(tokenBytes);

                await votoService.RealizarVoto(token, votoViewModel);
            }

            return View("Voto computado com sucesso." + Environment.NewLine + "Obrigado pela participação!");
        }
    }
}