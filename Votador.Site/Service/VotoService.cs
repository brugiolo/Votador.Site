using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<VotoViewModel> RealizarVoto(string token, VotoViewModel votoViewModel)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUri);
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("api/recurso/resultado");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                votoViewModel = await Task.Run(() => JsonConvert.DeserializeObject<VotoViewModel>(content));
                return votoViewModel;
            }
            else
            {
                return null;
            }
        }
    }
}
