using Fina.API.Data;
using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Fina.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Fina.API.Handlers
{
    public class CategoryHandler : ICategoryHandler
    {
        private readonly AppDbContext _context;
        public CategoryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Category>> CreateCategoryAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId
            };

            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria criada com sucesso");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Nao foi possivel criar a categoria");
            }
        }

        public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
        {


            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.CategoryId && request.UserId == c.UserId);

                if (category == null)
                {
                    return new Response<Category?>(null, 404, "Categoria nao encontrada");
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return new Response<Category?>(category, 200, "Categoria deletada com sucesso");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Nao foi possivel excluir a categoria");
            }
        }

        public async Task<Response<Category>> GeByIdAsync(GetCategoryById request)
        {
            try
            {
                var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.CategoriyId && request.UserId == c.UserId);

                return category is null
                    ? new Response<Category?>(null, 404, "Categoria nao encontrada")
                    : new Response<Category?>(category);

            }
            catch {
                return new Response<Category?>(null, 500, "Nao foi possivel recuperar a categoria");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategories request)
        {
            try
            {
                var querie = _context.Categories.AsNoTracking().Where(c => c.UserId == request.UserId).OrderBy(c => c.Title);

                var categories = await querie.Skip(request.PageSize * (request.PageNumber-1)).Take(request.PageSize).ToListAsync();

                var count = categories.Count();

                return new PagedResponse<List<Category>?>(categories,count,request.PageNumber,request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>?>(null, 500, "Nao foi possivel recuperar as categorias");
            }
        }

        public async Task<Response<Category>> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.CategoryId && c.UserId == request.UserId);

                if(category is null) 
                    return new Response<Category?>(null, 404, "Categoria nao encontrada");

                category.Title = request.Title;
                category.Description = request.Description;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                ;
                return new Response<Category?>(category,message:"Categoria atualizada com sucesso");
            }
            catch
            {
                return new Response<Category?>(null, 500, "nao foi possivel atualizar a categoria");
            }
        }
    }
}
