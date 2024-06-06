using Fina.Core.Enums;

namespace Fina.Core.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PaidOrReceivedAt { get; set; }

        public decimal Amount { get; set; }

        public TransactionType Type { get; set; } = TransactionType.WithDraw;
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;
    }
}
