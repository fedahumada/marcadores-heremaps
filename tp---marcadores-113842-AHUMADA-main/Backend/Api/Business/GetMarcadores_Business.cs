using System.Net;
using System.Net.Http.Headers;
using Api.Commands.Login;
using Api.Responses.Certificaciones;
using Api.Responses.Marcadores;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace Api.Business.Login;

public class GetMarcadores_Business
{
    public class GetMarcadoresComando : IRequest<ListadosMarcadores>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class EjecutaValidacion : AbstractValidator<GetMarcadoresComando>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username can't be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can't be empty");
        }
    }
    public class Manejador : IRequestHandler<GetMarcadoresComando, ListadosMarcadores>
    {
        static HttpClient httpClient = new HttpClient();
        private readonly IValidator<GetMarcadoresComando> _validator;

        public Manejador(IValidator<GetMarcadoresComando> validator)
        {
            _validator = validator;
        }

        public async Task<ListadosMarcadores> Handle(GetMarcadoresComando request, CancellationToken cancellationToken)
        {
            var result = new ListadosMarcadores();

            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = string.Join(Environment.NewLine, validation.Errors);
                result.SetMensajeError("",errors, HttpStatusCode.InternalServerError);
                return result;
            }

            string user = request.UserName;
            string pass = request.Password;
            
            var response = await httpClient.PostAsJsonAsync("https://prog3.nhorenstein.com/api/usuario/LoginUsuarioWeb",
                new{ nombreUsuario = user, password = pass });

            if (response.IsSuccessStatusCode)
            {
                List<ItemMarcador>? lst = new List<ItemMarcador>();
                var respuestaJsonLogin = await response.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Certification>(respuestaJsonLogin);
                lst = await GetMarcadoresConJwt("https://prog3.nhorenstein.com/api/marcador/GetMarcadores",respuesta.Token);
                
                foreach (var element in lst)
                {
                    var item = new ItemMarcador
                    {
                        Latitud = element.Latitud,
                        Longitud = element.Longitud,
                        Info = element.Info
                    };
                    
                    result.LitadoMarcadores.Add(item);

                }

                return result;
            }

            return null;
        }
        
        static async Task<List<ItemMarcador>> GetMarcadoresConJwt(string url, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var respuestaJsonMarcadores = await response.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<ListadosMarcadores>(respuestaJsonMarcadores);
                var lstMarcadores = new List<ItemMarcador>(respuesta.LitadoMarcadores);
                return lstMarcadores;

            }
            return null;
        }
            
    }
}
