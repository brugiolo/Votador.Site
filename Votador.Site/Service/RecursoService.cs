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
    public class RecursoService
    {
        public static string BaseUri = "https://localhost:44364/";

        public async Task<List<RecursoViewModel>> ObterApresentacao(string token)
        {
            var recursos = new List<RecursoViewModel>();

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUri);
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("api/recurso");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                recursos = await Task.Run(() => JsonConvert.DeserializeObject<List<RecursoViewModel>>(content));
            }

            return recursos;
        }

        public async Task<List<RecursoVotosViewModel>> ObterResultado(string token)
        {
            var recursosVotos = new List<RecursoVotosViewModel>();

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUri);
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("api/recurso/resultado");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                recursosVotos = await Task.Run(() => JsonConvert.DeserializeObject<List<RecursoVotosViewModel>>(content));
            }

            return recursosVotos;
        }
    }
}
