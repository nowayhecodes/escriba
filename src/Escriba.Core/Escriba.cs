using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;

namespace Escriba.Core
{
    public class Escriba
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly string _logFilePath;

        public Escriba(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<Escriba>();
        }

        public Escriba(RequestDelegate next, ILoggerFactory loggerFactory, string logFilePath = "")
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<Escriba>();
            _logFilePath = logFilePath;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation(await FormatRequest(context.Request));
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);

                _logger.LogInformation(await FormatResponse(context.Response));
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();
            var body = request.Body;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            // File.AppendAllText(_logFilePath, $"[{DateTime.Now} Request][{request.Method}] {request.Host} {request.Path} {request.QueryString} {request.ContentType} \n {bodyAsText} \n");
            Console.WriteLine($"[{DateTime.Now} Request][{request.Method}] {request.Host} {request.Path} {request.QueryString} {request.ContentType} \n {bodyAsText}");

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            // File.AppendAllText(_logFilePath, $"[{DateTime.Now} Response] {response.StatusCode} {response.ContentLength} {responseText} \n\n");
            Console.WriteLine($"[{DateTime.Now} Response] {response.StatusCode} {response.ContentLength} {responseText} \n\n");

            return $"Response {responseText}";
        }
    }
}
