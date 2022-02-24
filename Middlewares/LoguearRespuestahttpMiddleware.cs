using Microsoft.AspNetCore.Http;


namespace WebAPIAutores.Middlewares;

public class LoguearRespuestahttpMiddleware
{
    private readonly RequestDelegate siguente;
    private readonly ILogger<LoguearRespuestahttpMiddleware> logger;

    public  LoguearRespuestahttpMiddleware(RequestDelegate siguente, ILogger<LoguearRespuestahttpMiddleware> logger)
    {
        this.siguente = siguente;
        this.logger = logger;
        
    }

    //invoke o InvokeAsync

    public async Task InvokeAsync(HttpContext contexto){
        using (var ms = new MemoryStream())
        {
            var cuerpoOriginalRespuesta = contexto.Response.Body;
            contexto.Response.Body = ms;

            await siguente(contexto);

            ms.Seek(0, SeekOrigin.Begin);
            string respuesta = new StreamReader(ms).ReadToEnd();
            ms.Seek(0, SeekOrigin.Begin);

            await ms.CopyToAsync(cuerpoOriginalRespuesta);
            contexto.Response.Body = cuerpoOriginalRespuesta;

            logger.LogInformation(respuesta);
        }
    }
    
}
