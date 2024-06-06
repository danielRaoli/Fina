using Fina.API.Common;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Fina.API.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapDelete("/{id}",HandleAsync)
            .WithName("Transaction: DeleteTransaction")
            .WithSummary("Deleta uma transacao")
            .WithDescription("Deleta uma transacao")
            .WithOrder(5)
            .Produces<Response<Transaction?>>();

        public static async Task<IResult> HandleAsync([FromServices] ITransactionHandler handler, [FromRoute]int id)
        {
            var request = new DeleteTransactionRequest { Id = id, UserId = ApiConfiguration.UserId };

            var result = await handler.DeleteAsync(request);

            return result.IsSucces ? TypedResults.Ok(result) : TypedResults.NotFound(result);
        }
    }
}
