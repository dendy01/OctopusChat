using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Database;
using Server.Database.Entities;

namespace Server.Api.Controllers
{
	public class MessagesModel
	{
		public Message[] Messages { get; set; }
	}

	public class MessageModel
	{
		public string Text { get; set; }
		public DateTime DateTime { get; set; }
		public long ChatId { get; set; }
		public long UserId { get; set; }
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
		public async Task<IActionResult> SendMessage([FromBody] MessageModel model)
		{
			if (String.IsNullOrEmpty(model.Text))
			{
				return BadRequest("Message text is empty.");
			}

			Message entity = new()
			{
				Text = model.Text,
				DateTime = model.DateTime,
				ChatId = model.ChatId,
				UserId = model.UserId,
			};
			
			await _database.Messages.AddAsync(entity);
			await _database.SaveChangesAsync();
			
			return Ok();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<MessagesModel>> GetMessage(int id)
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
			
			return new MessagesModel()
			{
				Messages = messages.ToArray()
			};
		}
	}
}