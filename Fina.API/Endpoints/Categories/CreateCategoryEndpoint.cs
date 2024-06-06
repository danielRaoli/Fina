using Fina.API.Common;
using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Fina.API.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Criar uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync([FromServices]ICategoryHandler categoryHandler,[FromBody]CreateCategoryRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
           var response =  await categoryHandler.CreateCategoryAsync(request);

            return response.IsSucces
                ? TypedResults.Created($"v1/categories/{response.Data.Id}", response)
                : TypedResults.BadRequest(response);
        }
    }
}
