using Application.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Application.UserServices
{
    public class UserServiceViaje : IUserServiceViaje
    {
        private readonly HttpClient _httpClient;

        public UserServiceViaje()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198/api/");
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

        public dynamic ModificarViaje(int viajeId, int asientosDisponibles)
        {

            HttpResponseMessage response = _httpClient.PutAsync($"Viaje/{viajeId}?asientosDisponibles={Uri.EscapeDataString(asientosDisponibles.ToString())}", null).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
            else
            {
                throw new Exception($"Error al modificar el viaje. Código de respuesta: {response.StatusCode}");
            }
        }
    }
}
