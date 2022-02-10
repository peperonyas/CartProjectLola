using Cart.Core.Model;
using Cart.Core.Utilities;
using Newtonsoft.Json;
using System.Net;

namespace Cart.Api.Middleware
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await _next(context);

                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                object objResult;

                if (readToEnd.ValidateJSON())
                {
                    objResult = JsonConvert.DeserializeObject(readToEnd);
                }
                else
                {
                    objResult = readToEnd;
                }
                string errorMessage = string.Empty;
                if (context.Items["exceptionMessage"] != null)
                {
                    errorMessage = context.Items["exceptionMessage"].ToString();
                }
                string message = string.Empty;
                if (context.Items["message"] != null)
                {
                    message = context.Items["message"].ToString();
                }
                var result = ApiResponse.Create((HttpStatusCode)context.Response.StatusCode, objResult, message, errorMessage);
                context.Response.ContentType = "application/json";
                context.Response.Headers.ContentLength = null;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
