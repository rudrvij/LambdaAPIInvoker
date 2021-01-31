using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

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
        public async Task<bool> InvokeApi(string payload)
        {
            string url = "/";
            logger.LogInformation($"Hitting with the following data {payload}");
                        
            var response = await this.httpClient.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"ERROR");
                throw new HttpRequestException("Error");
            }
                

            logger.LogInformation($"Successful");

            return true;

        }
    }
}
