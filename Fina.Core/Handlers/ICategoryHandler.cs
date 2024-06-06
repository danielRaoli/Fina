using Fina.Core.Entities;
using Fina.Core.Requests.Categories;
using Fina.Core.Response;

namespace Fina.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category>> GeByIdAsync(GetCategoryById request);
        Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategories request);
        Task<Response<Category>> UpdateCategoryAsync(UpdateCategoryRequest request);
        Task<Response<Category>> CreateCategoryAsync(CreateCategoryRequest request);

    }
}
