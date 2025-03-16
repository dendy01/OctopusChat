using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Api.Controllers;
using Server.Database;
using Server.Database.Entities;

namespace Server.Api
{
	public class RoomService
	{
		public readonly Dictionary<string, string> Room = new();
	}
	//[Authorize]
	public class ChatHub : Hub
	{
		private readonly ChatDatabase _database;
		private readonly AccountConfiguration _accountConfiguration;
		private readonly RoomService _roomService;
		
		public ChatHub(ChatDatabase database,
			IOptions<AccountConfiguration> accountConfiguration,
			RoomService roomService)
		{
			_database = database;
			_accountConfiguration = accountConfiguration.Value;
			_roomService = roomService;
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
		
		public override async Task OnConnectedAsync()
		{
			HttpContext httpContext = Context.GetHttpContext();
			
			string token = httpContext.Request.Query["access_token"].ToString();

			string userId = GetUserIdFromToken(token);

			_roomService.Room.Add(Context.ConnectionId, userId);
			_roomService.Room.Add(userId, Context.ConnectionId);
			
			await base.OnConnectedAsync();
		}
		
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			string userId = _roomService.Room[Context.ConnectionId];
			
			_roomService.Room.Remove(Context.ConnectionId);
			_roomService.Room.Remove(userId);
			
			await base.OnDisconnectedAsync(exception);
		}
		
		public async Task SendMessage(MessageModel message)
		{
			if (String.IsNullOrEmpty(message.Text))
			{
				await Clients.All.SendAsync("IsReceived", null, null);
				
				return;
			}

			string userId = _roomService.Room[Context.ConnectionId];
			
			Message entity = new()
			{
				Text = message.Text,
				CreatedDateTime = message.CreatedDateTime,
				ChatId = message.ChatId,
				UserId = Convert.ToInt64(userId),
			};

			List<string> ids = _database.ChatMembers
				.Where(chat => chat.ChatId == message.ChatId)
				.Select(chat => chat.UserId.ToString()).ToList();

			List<string> connectedIds = ids
				.Where(id => _roomService.Room.ContainsKey(id))
				.Select(id => _roomService.Room[id])
				.ToList();

			
			await _database.Messages.AddAsync(entity);
			await _database.SaveChangesAsync();
			
			await Clients.Clients(connectedIds).SendAsync("ReceiveMessage", entity);
			
			await Clients.Client(_roomService.Room[userId]).SendAsync("IsReceived", entity);
		}
	}
}