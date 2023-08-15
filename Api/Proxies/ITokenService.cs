using System.Threading.Tasks;

namespace Crew.Api.ReferenceImpl.V1.Proxies
{
    public interface ITokenService
    {
        Task<string> GetToken();
    }
}