using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Titulo invalido")]
        [MaxLength(80, ErrorMessage = "o titulo deve conter ate 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage ="Descrição invalida")]
        public string Description { get; set; } = string.Empty;
    }
}
