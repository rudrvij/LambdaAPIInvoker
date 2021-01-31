using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace LambdaAPIInvoker
{
    public class ApiInvoker : IApiInvoker
    {
        private readonly ILogger<ApiInvoker> logger;
        private readonly HttpClient httpClient;
        public ApiInvoker(ILogger<ApiInvoker> logger, HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
        }
        public bool InvokeApi(string payload)
        {
            logger.LogInformation($"Logging Information");
            logger.LogWarning($"Logging Warning");
            logger.LogError($"Logging Error");
            logger.LogCritical($"Logging Critical");

            return true;
        }
    }
}
