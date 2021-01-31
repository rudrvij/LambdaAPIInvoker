using System;
using System.Net.Http.Headers;

using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaAPIInvoker
{
    public class Function
    {
        public IConfigurationService ConfigurationService { get;  }
        private readonly IApiInvoker apiInvoker;
        private ServiceCollection serviceCollection;

        public Function()
        {
            //Set up Dependency Injection
            serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            ConfigurationService = serviceProvider.GetService<IConfigurationService>();
            apiInvoker = serviceProvider.GetService<IApiInvoker>();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConfigurationService, ConfigurationService>();            
            serviceCollection.AddLogging((logging =>
            {
                logging.AddLambdaLogger();
                logging.SetMinimumLevel(LogLevel.Debug);
            }));
            serviceCollection.AddHttpClient<IApiInvoker, ApiInvoker>(client =>
            {
                string baseAddress = ConfigurationService.GetConfiguration()["APIAddress"];
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {
            apiInvoker.InvokeApi(input);
            return "Completed";
        }
    }
}
