using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace LambdaAPIInvoker
{
    public class ConfigurationService : IConfigurationService
    {
        public IConfiguration GetConfiguration()
        {
            string environmentName = Environment.GetEnvironmentVariable(Constants.AspnetCoreEnvironment) ?? Constants.ProductionEnvironment;
            return new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
           .AddEnvironmentVariables()
           .Build();
        }
    }
}
