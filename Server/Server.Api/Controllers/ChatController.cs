using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Database;
using Server.Database.Entities;

namespace Server.Api.Controllers
{
	public class ChatResponse
	{
		public Message[] Messages { get; set; }
	}

	[ApiController]
	[Route("chat")]
	public class ChatController : ControllerBase
	{
		private readonly ChatDatabase _database;

		public ChatController(ChatDatabase database)
		{
			_database = database;
		}

		[HttpPost("send")]
		public async Task<IActionResult> SendMessage(Message message)
		{
			if (String.IsNullOrEmpty(message.Text))
			{
				return BadRequest("Message text is empty.");
			}
			
			await _database.Messages.AddAsync(message);
			await _database.SaveChangesAsync();
			
			return Ok();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ChatResponse>> GetMessage(int id)
		{
			Chat chat = _database.Chats.FirstOrDefault();

			if (chat is null)
			{
				return NotFound("Doest not exist chats.");
			}

			List<Message> messages = await _database.Messages
				.OrderBy(message => message.DateTime)
				.Where(message => message.ChatId == chat.Id)
				.Take(20)
				.ToListAsync();
			
			return new ChatResponse()
			{
				Messages = messages.ToArray()
			};
		}
	}
}