using Application.Interfaces;

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

        public dynamic ObtenerUsuario(Guid usuarioId)
        {
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
    }
}