using Fina.API.Common;
using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fina.API.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get")
            .WithDescription("Busca uma categoria pelo seu id")
            .WithSummary("Busca uma categoria pelo id")
            .WithOrder(4)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync([FromServices] ICategoryHandler handler, [FromRoute]int id)
        {
            var request = new GetCategoryById { UserId = ApiConfiguration.UserId, CategoriyId = id };

            var result = await handler.GeByIdAsync(request);

            return result.IsSucces ? TypedResults.Ok(result) : TypedResults.NotFound();                
        }
    }
}
