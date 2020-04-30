using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Votador.Site.Models;

namespace Votador.Site.Service
{
    public class LoginService
    {
        public static string BaseUri = "https://localhost:44364/";

        public async Task<LoginResultViewModel> Autenticar(LoginViewModel loginViewModel)
        {
            var conteudo = JsonConvert.SerializeObject(loginViewModel);
            var httpconteudo = new StringContent(conteudo, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUri);
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            httpClient.DefaultRequestHeaders.Accept.Clear();

            var responseToken = await httpClient.PostAsync("api/login/autenticar", httpconteudo);

            if (responseToken.IsSuccessStatusCode)
            {
                var content = await responseToken.Content.ReadAsStringAsync();
                loginViewModel = await Task.Run(() => JsonConvert.DeserializeObject<LoginViewModel>(content));
            }

            return new LoginResultViewModel
            {
                Login = loginViewModel,
                StatusCode = responseToken.StatusCode
            };
        }
    }
}
