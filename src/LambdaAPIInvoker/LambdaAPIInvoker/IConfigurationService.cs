using Microsoft.Extensions.Configuration;

namespace LambdaAPIInvoker
{
    public interface IConfigurationService
    {
        IConfiguration GetConfiguration();
    }
}
