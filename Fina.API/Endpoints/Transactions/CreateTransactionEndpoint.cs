using Fina.API.Common;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Fina.API.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
            .WithName("Transaction: CreateTransaction")
            .WithSummary("Cria uma transacao")
            .WithDescription("Cria uma transacao")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();
            

        public static async Task<IResult> HandleAsync([FromServices] ITransactionHandler handler,[FromBody] CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;

            var result = await handler.CreateAsync(request);

            return result.IsSucces ? TypedResults.Ok(result) : TypedResults.BadRequest(result); 
        }
    }
}
