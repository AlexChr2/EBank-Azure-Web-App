namespace Backend.Models
{
	public enum TransactionType
	{
		Deposit,
		Withdrawal,
		Transfer
	}

	public class Transaction
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public TransactionType Type { get; set; }
		public int SourceCardId { get; set; }
		public int? RecipientCardId { get; set; }
		public decimal Amount { get; set; }
		public string? Description { get; set; }
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
	}
}
