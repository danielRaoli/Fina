using Fina.API.Common;
using Fina.Core;
using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fina.API.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Busca todas as categorias")
            .WithDescription("Busca todas as categorias")
            .WithOrder(5)
            .Produces<PagedResponse<Category?>>();

        private static async Task<IResult> HandleAsync([FromServices] ICategoryHandler handler, [FromQuery] int pageNumber = Configuration.DefaultPageNumber, 
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategories { UserId = ApiConfiguration.UserId, PageNumber = pageNumber, PageSize = pageSize};

            var result = await handler.GetAllAsync(request);

            return result.IsSucces ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

    }
}
