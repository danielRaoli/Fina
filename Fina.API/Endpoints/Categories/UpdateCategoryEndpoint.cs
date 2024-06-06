using Fina.API.Common;
using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fina.API.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update Categorie")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync([FromServices] ICategoryHandler  handler,[FromBody] UpdateCategoryRequest request,[FromRoute] int id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.CategoryId = id;

            var result =await  handler.UpdateCategoryAsync(request);

            return result.IsSucces? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
