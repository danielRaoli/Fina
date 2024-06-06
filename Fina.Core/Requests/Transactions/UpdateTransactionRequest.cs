using Fina.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transactions
{
    public class UpdateTransactionRequest : Request
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titulo invalido")]
        [MaxLength(80, ErrorMessage = "o titulo deve conter ate 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantia invalida")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Data invalida")]
        public DateTime PaidOrReceivedAt { get; set; }

        [Required(ErrorMessage = "Tipo invalido")]
        public TransactionType Type { get; set; }

        [Required(ErrorMessage = "Categoria invalida")]
        public int CategoryId { get; set; }
    }
}
