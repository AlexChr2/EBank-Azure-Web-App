using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
	public class ECard
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int UserId { get; set; }
		[Required]
		public string Name { get; set; } = "";
		public decimal CashAmount { get; set; } = 0;
	}
}
