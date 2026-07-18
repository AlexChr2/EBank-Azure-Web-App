using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Backend.DTOs;

namespace Backend.Controllers
{
	public class RequireUserLoggedInAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var userID = context.HttpContext.Session.GetInt32("UserID");
			if (userID == null)
			{
				context.Result = new UnauthorizedObjectResult(
					"User not logged in!"
				);
			}
		}
	}

	[Route("api")]
	[ApiController]
	public class MainController : ControllerBase
	{
		private readonly PasswordHasher<User> passwordHasher;

		public MainController()
		{
			passwordHasher = new PasswordHasher<User>();
		}

		[HttpPost("register-user")]
		public async Task<IActionResult> RegisterUser([FromBody] AuthRequest request)
		{
			/*
			if (await context.Users.AnyAsync(
				u => u.Email == request.Email)
			)
				return BadRequest("User with the same credentials already exists!");
			*/

			User user = new()
			{
				Email = request.Email
			};
			user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

			// context.Users.Add(user);
			// await context.SaveChangesAsync();

			return Ok(new { Message = "User registered successfully!" });
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginUser([FromBody] AuthRequest request)
		{
			/*
			var user = await context.Users.FirstOrDefaultAsync(
				u => u.Email == request.Email);

			if (user != null)
			{
				var result = passwordHasher.VerifyHashedPassword(
					user, user.PasswordHash, request.Password);

				if (result == PasswordVerificationResult.Success)
				{
					HttpContext.Session.SetInt32("UserID", user.Id);
					return Ok(new { Message = "Login successful!" });
				}
			}
			return BadRequest("Incorrect email or password.");
			*/

			// temporary until Azure comes into play
			HttpContext.Session.SetInt32("UserID", 0);
			return Ok("Success!");
		}

		[HttpGet("logout")]
		public Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();
			return Task.FromResult<IActionResult>(Ok("Logout successful."));
		}

		// create a new card
		[RequireUserLoggedIn]
		[HttpPost("card")]
		public async Task<IActionResult> CreateCard([FromBody] CreateCardRequest request)
		{
			if (request.Name.Length == 0)
			{
				return BadRequest("Empty card name!");
			}

			ECard card = new()
			{
				UserId = (int) HttpContext.Session.GetInt32("UserID"),
				Name = request.Name
			};

			//context.Cards.Add(card);
			//await context.SaveChangesAsync();
			return Ok("Card creation successful!");
		}

		// rename a card
		/*[RequireUserLoggedIn]
		[HttpPut("card")]
		public async Task<IActionResult> RenameCard([FromBody] RenameCardRequest request)
		{
			// TODO: select the card from the db that matches with request.CardID
			// then change its name and save changes async
		}*/

		[RequireUserLoggedIn, HttpDelete("card")]
		public async Task<IActionResult> DeleteCard([FromBody] DeleteCardRequest request)
		{
			// TODO: select the card from the db that matches with request.CardID
			// then make all of its transactions from the db "anonymous" (aka making
			// the card_id and recipient_id set to -1 for all transactions,
			// and then delete the card itself
		}

		[RequireUserLoggedIn, HttpGet("get-cards")]
		public async Task<IActionResult> GetCards()
		{
			/*var Cards = await context.Cards
				.Where(c => c.UserID == HttpContext.Session.GetInt32("UserID"))
				.ToListAsync();

			return Ok(Cards);*/

			// temporary data
			return Ok(new List<ECard>
			{
				new() { UserId = 0, Id = 0, Name = "Piraeus" },
				new() { UserId = 0, Id = 1, Name = "Eurobank" },
			});
		}

		// make transaction
		// get transactions
		// get users
		// delete user
	}
}
