using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		// TODO: add a username
		[Required]
		public string Email { get; set; } = "";
		public string PasswordHash { get; set; } = "";
		public List<ECard> Cards { get; set; } = [];
		public List<Transaction> Transactions { get; set; } = [];
	}
}
