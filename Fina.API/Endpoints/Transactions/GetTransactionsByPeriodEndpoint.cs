using Fina.API.Common;
using Fina.Core;
using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transactions;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fina.API.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
            .WithName("Transaction: GetTransactionByPeriod")
            .WithSummary("Pega todas as transacoes entre um determinado periodo")
            .WithDescription("Pega todas as transacoes entre um determinado periodo")
            .WithOrder(4)
            .Produces<PagedResponse<Transaction?>>();

        public static async Task<IResult> HandleAsync(
            [FromServices] ITransactionHandler handler,
            [FromQuery]int pageSize = Configuration.DefaultPageSize,
            [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] DateTime? dateStart = null, 
            [FromQuery] DateTime? dateEnd = null)
        {
            var request = new GetTransactionsByPeriodRequest { UserId = ApiConfiguration.UserId,PageSize = pageSize, PageNumber = pageNumber, StartDate = dateStart, EndDate = dateEnd };

            var result = await handler.GetByPeriodAsync(request);

            return result.IsSucces ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
