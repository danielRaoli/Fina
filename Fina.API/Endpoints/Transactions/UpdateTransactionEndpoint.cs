
using Fina.API.Common;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Fina.API.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{id}", HandleAsync).WithName("Transaction: UpdateTransaction")
            .WithSummary("atualiza uma transacao")
            .WithDescription("atualiza uma transacao")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();

        public static async Task<IResult> HandleAsync([FromServices] ITransactionHandler handler, [FromRoute] int id,[FromBody] UpdateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;
            
            var result = await handler.UpdateAsync(request);

            return result.IsSucces? TypedResults.Ok(result) : TypedResults.NotFound(result);
        }


    }
}
