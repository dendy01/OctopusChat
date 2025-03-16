using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Database;
using Server.Database.Entities;

namespace Server.Api.Controllers
{
	public class MessagesModel
	{
		public Dictionary<long, string> Members { get; set; }
		public Message[] Messages { get; set; }
	}
	public class MessageModel
	{
		public string Text { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public long ChatId { get; set; }
		public long UserId { get; set; }
	}

	public class UserModel
	{
		
	}
	
	[ApiController]
	[Route("chat")]
	public class ChatController : ControllerBase
	{
		private readonly AccountConfiguration _accountConfiguration;
		private readonly ChatDatabase _database;
		public ChatController(IOptions<AccountConfiguration> accountConfiguration, ChatDatabase database)
		{
			_accountConfiguration = accountConfiguration.Value;
			_database = database;
		}
	
		private string GetUserIdFromToken(string token)
		{
			JwtSecurityTokenHandler tokenHandler = new();
			byte[] key = Encoding.UTF8.GetBytes(_accountConfiguration.JwtKey);
			
			TokenValidationParameters parameters = new()
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
			return principal.FindFirstValue(ClaimTypes.NameIdentifier);
		}
		
		//[Authorize]
		[HttpPost("send")]
		public async Task<IActionResult> SendMessage(
			[FromHeader(Name = "Authorization")] string jwtToken,
			[FromBody] MessageModel model)
		{
			if (String.IsNullOrEmpty(model.Text))
			{
				return BadRequest("Message text is empty.");
			}

			string token = jwtToken.Split(" ")[1];
			string userId = GetUserIdFromToken(token);
			
			Message entity = new()
			{
				Text = model.Text,
				CreatedDateTime = model.CreatedDateTime,
				ChatId = model.ChatId,
				UserId = Convert.ToInt64(userId),
			};
			
			await _database.Messages.AddAsync(entity);
			await _database.SaveChangesAsync();
			
			return Ok();
		}
		
		//[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<MessagesModel>> GetMessages(int id)
		{
			Chat chat = await _database.Chats.Include(chat => chat.Members).FirstOrDefaultAsync();

			if (chat is null)
			{
				return NotFound($"Doest not exist chats by id: {id}.");
			}

			Message[] messages = await _database.Messages
				.OrderByDescending(message => message.CreatedDateTime)
				.Where(message => message.ChatId == chat.Id)
				.Take(15)
				.OrderBy(message => message.Id)
				.ToArrayAsync();

			HashSet<long> usersIds = messages.Select(message => message.UserId).ToHashSet();

			User[] members = _database.Users.Where(user => usersIds.Contains(user.Id)).ToArray();
			
			return new MessagesModel()
			{
				Members = members.ToDictionary(member => member.Id, member => member.UserName),
				Messages = messages
			};
		}
	}
}