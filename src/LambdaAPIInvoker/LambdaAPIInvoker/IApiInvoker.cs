using System.Threading.Tasks;

namespace LambdaAPIInvoker
{
    public interface IApiInvoker
    {
        Task<bool> InvokeApi(string payload);
    }
}
