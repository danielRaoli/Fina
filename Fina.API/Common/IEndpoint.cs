using Microsoft.AspNetCore.Routing;

namespace Fina.API.Common
{
    public interface IEndpoint
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}
