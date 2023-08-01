using System.Net;

namespace Api.Responses;

public class ResponseBase
{
    public bool Ok { get; set; } = true;
    public string Error { get; set; } = "";
    public string MensajeInfo { get; set; } = "";

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    public void SetMensajeError(string mensajeInfo, string error, HttpStatusCode statusCode)
    {
        Ok = false;
        Error = error;
        MensajeInfo = mensajeInfo;
        StatusCode = statusCode;
    }
}