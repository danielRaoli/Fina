using Fina.API.Common;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Fina.API.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/{id}",HandleAsync)
            .WithName("Transaction:  GetTransaction")
            .WithSummary("Busca uma transacao")
            .WithDescription("Busca uma transacao")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();
        public static async Task<IResult> HandleAsync([FromServices] ITransactionHandler handler, int id, [FromBody]GetTransactionByIdRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var result = await handler.GetByIdAsync(request);

            return result.IsSucces ? TypedResults.Ok(result) : TypedResults.NotFound(result);
        }
    }
}
