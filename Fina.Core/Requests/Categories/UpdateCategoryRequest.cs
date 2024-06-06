using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Titulo invalido")]
        [MaxLength(80, ErrorMessage ="o titulo deve conter ate 80 caracteres")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Descrição invalido")]
        [MaxLength(80, ErrorMessage = "a descrição deve conter ate 80 caracteres")]
        public string Description { get; set; }

    }
}
