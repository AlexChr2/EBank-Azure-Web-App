using Backend.Models;

namespace Backend.DTOs
{
	public record CardsResponse(string ID, string Name, uint Balance);
	public record TransactionResponse(
		int ID,
		TransactionType Type,
		int SourceCardID,
		int RecipientCardID,
		decimal Amount,
		string Description,
		DateTime Timestamp
	);

	// Request both for registering and logging in
	public record AuthRequest(string Email, string Password);

	public record CreateCardRequest(string Name);
	public record CreateCardResponse(string NewCardID);

	public record RenameCardRequest(int CardID, string NewName);

	public record DeleteCardRequest(int CardID);

	public record GetCardsResponse(List<CardsResponse> Cards);

	public record MakeTransactionRequest(
		TransactionType TransactionType,
		int SourceCardID,
		int RecipientCardID,
		int Amount,
		string Description
	);

	public record GetTransactionsResponse(List<TransactionResponse> Transactions);

	public record DeleteUserRequest(int UserID);
}
