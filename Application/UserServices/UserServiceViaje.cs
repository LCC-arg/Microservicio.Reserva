using Application.Interfaces;

namespace Application.UserServices
{
    public class UserServiceViaje : IUserServiceViaje
    {
        private readonly HttpClient _httpClient;

        public UserServiceViaje()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7192/api/");
        }

        public dynamic ObtenerViaje(int viajeId)
        {
            HttpResponseMessage response = _httpClient.GetAsync($"Viaje/{viajeId}").Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic viaje = response.Content.ReadAsAsync<dynamic>().Result;
                return viaje;
            }
            else
            {
                throw new Exception($"Error al obtener el Viaje. Código de respuesta: {response.StatusCode}");
            }
        }
    }
}
