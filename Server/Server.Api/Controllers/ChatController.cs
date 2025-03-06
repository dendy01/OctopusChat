using Microsoft.AspNetCore.Mvc;

namespace Server.Api.Controllers
{
	public class Message
	{
		public long Id { get; set; }
		public string Text { get; set; }
		public DateTime DateTime { get; set; }
		public long UserId { get; set; }
	}

	public class User
	{
		public long Id { get; set; }
		public string Name { get; set; }
	}

	public class MessagesResponse
	{
		public User[] Members { get; set; }
		public Message[] Messages { get; set; }
	}
	
	[ApiController]
	[Route("chat")]
	public class ChatController : ControllerBase
	{
		[HttpGet("{id}")]
		public ActionResult<MessagesResponse> GetMessage(int id)
		{
			return new MessagesResponse()
			{
				Members = new User[]
				{
					new User() { Id = 1, Name = "Yuriy" },
					new User() { Id = 2, Name = "Evgeniy" },
				},
				Messages = new Message[]
				{
					new Message()
					{
						Id = 0,
						Text = "Здорова, ну что? что делаешь?",
						UserId = 1,
						DateTime = new (2025, 03, 05, 17, 05, 00),
					},
					new Message()
					{
						Id = 1,
						Text = "Ты уже пришёл с работы?",
						UserId = 2,
						DateTime = new (2025, 03, 05, 18, 05, 30),
					},
					new Message()
					{
						Id = 2,
						Text = "ещё не пришёл, но я закончил задачу и сижу писюн пинаю",
						UserId = 1,
						DateTime = new (2025, 03, 05, 18, 06, 20),
					},
					new Message()
					{
						Id = 3,
						Text = "Ахахахахах",
						UserId = 2,
						DateTime = new (2025, 03, 05, 18, 06, 40),
					},
					new Message()
					{
						Id = 4,
						Text = "Ну что? давай чат сделаем? я думаю уйдёт дня 2-3 на базовые вещи.\n\nВопрос первый - кто будем лидом?",
						UserId = 1,
						DateTime = new (2025, 03, 05, 18, 07, 00),
					},
					new Message()
					{
						Id = 5,
						Text = "Я считаю что литы должны быть бэкендеры, так как на них больше ответственности",
						UserId = 2,
						DateTime = new (2025, 03, 05, 18, 08, 10),
					},
					new Message()
					{
						Id = 6,
						Text = "правильно",
						UserId = 1,
						DateTime = new (2025, 03, 05, 18, 08, 50),
					}
				}
			};
		}
	}
}