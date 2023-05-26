using Application.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Application.UseServices
{
    public class UserServiceUsuario : IUserServiceUsuario
    {
        private readonly HttpClient _httpClient;

        public UserServiceUsuario()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7196/api/");
        }
        public dynamic ObtenerUsuario(Guid usuarioId, string token)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = _httpClient.GetAsync($"Usuario/{usuarioId}").Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic usuario = response.Content.ReadAsAsync<dynamic>().Result;
                return usuario;
            }
            else
            {
                throw new Exception($"Error al obtener el Usuario. Código de respuesta: {response.StatusCode}");
            }
        }

        public string ObtenerToken(string email, string contraseña)
        {

            var diccionario = new Dictionary<string, string>
            {
                {"email", email},
                {"password",contraseña}
            };

            string json = JsonConvert.SerializeObject(diccionario);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync($"Usuario/login", data).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
            else
            {
                throw new Exception($"Error al obtener el Token. Código de respuesta: {response.StatusCode}");
            }
        }
    }
}