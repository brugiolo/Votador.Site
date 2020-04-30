using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Votador.Site.Models;

namespace Votador.Site.Service
{
    public class VotoService
    {
        public static string BaseUri = "https://localhost:44364/";

        public async Task<VotoViewModel> ObterVoto(string token, int idVoto)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUri);
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("api/voto/resultado");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var votoViewModel = await Task.Run(() => JsonConvert.DeserializeObject<VotoViewModel>(content));
                return votoViewModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> RealizarVoto(string token, VotoViewModel votoViewModel)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUri);
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var voto = new
            {
                Id = 0,
                UsuarioId = votoViewModel.UsuarioId,
                RecursoId = votoViewModel.Recurso.Id,
                Comentario = votoViewModel.Comentario
            };

            var conteudo = JsonConvert.SerializeObject(voto);
            var httpconteudo = new StringContent(conteudo, Encoding.UTF8, "application/json");

            var mensagem = string.Empty;
            var response = await httpClient.PostAsync("api/voto", httpconteudo);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    mensagem = "Voto realizado com sucesso, obrigado pela participação!";
                    break;
                case HttpStatusCode.Conflict:
                    mensagem = "A regra é clara, só é possível votar uma vez em cada ideia.";
                    break;
                default:
                    mensagem = "Desculpe, falha ao registrar o voto, tente novamente mais tarde.";
                    break;
            }

            return mensagem ;
        }
    }
}
