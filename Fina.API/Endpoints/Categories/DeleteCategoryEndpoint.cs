using Fina.API.Common;
using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fina.API.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Exclui uma categoria")
            .WithDescription("exclui uma categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();
           


        private static async Task<IResult> HandleAsync([FromServices]ICategoryHandler handler,[FromRoute] int id)
        {
            var request = new DeleteCategoryRequest { UserId = ApiConfiguration.UserId, CategoryId = id };

            var result = await handler.DeleteAsync(request);

            return result.IsSucces ? TypedResults.Ok(result) : TypedResults.NotFound(result);
        }
    }
}
