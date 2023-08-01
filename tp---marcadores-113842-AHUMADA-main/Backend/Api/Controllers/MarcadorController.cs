using Api.Business.Login;
using Api.Responses.Marcadores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class MarcadorController : ControllerBase
{
    private readonly IMediator _mediator;

    public MarcadorController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("api/GetMarcadores/credenciales/{username}-{password}")]
    public async Task<ListadosMarcadores> GetMarcadores(string username, string password)
    {
        return await _mediator.Send(new GetMarcadores_Business.GetMarcadoresComando()
        {
            UserName = username,
            Password = password
        });
    }
}