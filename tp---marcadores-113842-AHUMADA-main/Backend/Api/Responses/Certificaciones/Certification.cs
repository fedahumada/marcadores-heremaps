namespace Api.Responses.Certificaciones;

public class Certification : ResponseBase
{
    public string IdUsuario { get; set; } 
    public string Token { get; set; } 
    public string EmailUsuario { get; set; } 
    public string UrlImagen { get; set; } 
    public string Nombre { get; set; } 
    public string Apellido { get; set; }
    public int IdRol { get; set; } 
    public string RolName { get; set; }

}