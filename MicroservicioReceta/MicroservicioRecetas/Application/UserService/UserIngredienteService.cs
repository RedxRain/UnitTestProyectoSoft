using Application.Exceptions;
using Application.Interfaces.Services;
using Newtonsoft.Json.Linq;

namespace Application.UserService
{
    public class UserIngredienteService : IUserIngredienteService
    {
        private readonly HttpClient _httpClient;
        private readonly JwtTokenService _jwtTokenService;
        private string _token;
        public UserIngredienteService(JwtTokenService jwtTokenService)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7192/api/");
            _jwtTokenService = jwtTokenService;
            EstablecerToken();
        }

        public void EstablecerToken()
        {
            _token = _jwtTokenService.Generartoken();//Genera token.

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.
                AuthenticationHeaderValue("Bearer", _token); //Colocar token en solicitud HTTP.
        }

        public dynamic GetByID(int Id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync($"Ingrediente/ById/{Id}").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ExceptionNotFound("No existe el ingrediente con el id: ");
                }
                dynamic ingrediente = response.Content.ReadAsAsync<dynamic>().Result;

                return ingrediente;
            }
            catch (ExceptionNotFound e)
            {
                throw new ExceptionNotFound(e.Message);
            }
        }

        public string GetIngredienteName(int Id)
        {
            HttpResponseMessage response = _httpClient.GetAsync($"Ingrediente/ById/{Id}").Result;
            string jsonContent = response.Content.ReadAsStringAsync().Result;
            dynamic dynamicData = JObject.Parse(jsonContent);
            return ($"{dynamicData.name}");
        }

        public dynamic GetByName(string Name)
        {
            HttpResponseMessage response = _httpClient.GetAsync($"Ingrediente/{Name}").Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic ingrediente = response.Content.ReadAsAsync<dynamic>().Result;
                return ingrediente;
            }
            else
            {
                throw new ArgumentException($"Error al obtener el ingrediente. Código de respuesta: {response.StatusCode}");
            }
        }
    }
}