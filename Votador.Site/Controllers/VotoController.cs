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
            HttpContext.Session.TryGetValue("usuario.id", out byte[] idBytes);

            var token = Encoding.Default.GetString(tokenBytes);
            var email = Encoding.Default.GetString(emailBytes);
            var usuarioId = Encoding.Default.GetString(idBytes);

            var recursoService = new RecursoService();
            var recursos = await recursoService.ObterApresentacao(token);
            var recurso = recursos.FirstOrDefault(r => r.Id == recursoId);

            var votoViewModel = new VotoViewModel
            {
                UsuarioId = Convert.ToInt32(usuarioId),
                UsuarioEmail = email,
                Comentario = string.Empty,
                Recurso = recurso
            };

            return View(votoViewModel);
        }

        public async Task<IActionResult> ConfirmarVoto(VotoViewModel votoViewModel)
        {
            var mensagem = string.Empty;

            var tokenValido = HttpContext.Session.TryGetValue("token", out byte[] tokenBytes);
            if (tokenValido)
            {
                var votoService = new VotoService();
                var token = Encoding.Default.GetString(tokenBytes);

                mensagem = await votoService.RealizarVoto(token, votoViewModel);
            }

            return RedirectToAction("Index", "Home", new { mensagem = mensagem });
        }
    }
}