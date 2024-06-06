using Fina.App;
using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Fina.Core.Response;
using System.Net.Http.Json;

namespace Fina.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClient) : ICategoryHandler

    {
        private readonly HttpClient _client = httpClient.CreateClient(AppConfiguration.HttpClientName);
        public const string endpoint = "v1/categories";
        public async Task<Response<Category>> CreateCategoryAsync(CreateCategoryRequest request)
        {
            request.UserId = "daniel@gmail.com";
            var result = await _client.PostAsJsonAsync(endpoint, request);

            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null,400,"nao foi possivel criar a categoria");
        }

        public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
        {
            var result = await _client.DeleteAsync(endpoint+$"/{request.CategoryId}");
            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null, 400, "falha ao excluir categoria");
        }

        public async Task<Response<Category>> GeByIdAsync(GetCategoryById request)
        {
            var result = await _client.GetAsync(endpoint + $"/{request.CategoriyId}");

            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null, 400, "nao foi possivel obter a categoria");
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategories request)
        {
            var result = await _client.GetAsync(endpoint);

            return await result.Content.ReadFromJsonAsync<PagedResponse<List<Category?>>>() ?? new PagedResponse<List<Category?>>(null, 400,"nao foi possivel obter as categorias");
        }

        public async Task<Response<Category>> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            var result = await _client.PutAsJsonAsync(endpoint + $"/{request.CategoryId}", request);
            return await result.Content.ReadFromJsonAsync<Response<Category>>();
        }
    }
}
