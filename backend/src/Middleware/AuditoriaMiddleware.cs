using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace myApp.Middleware
{
    public class AuditoriaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditoriaMiddleware> _logger;
        
        public AuditoriaMiddleware(RequestDelegate next, ILogger<AuditoriaMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            // Loga detalhes da requisição
            _logger.LogInformation($"[Auditoria] Requisição: {context.Request.Method} {context.Request.Path}");
            
            await _next(context);
            
            // Loga detalhes da resposta
            _logger.LogInformation($"[Auditoria] Resposta: {context.Response.StatusCode}");
        }
    }
}
