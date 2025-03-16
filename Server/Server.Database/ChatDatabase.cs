using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Server.Database
{
	public sealed class ChatDatabase : IdentityDbContext<User, IdentityRole<long>, long>
	{
		public DbSet<Message> Messages { get; set; }
		public DbSet<Chat> Chats { get; set; }
		public DbSet<ChatMember> ChatMembers { get; set; } 
		public ChatDatabase()
		{
			
		}
		public ChatDatabase(DbContextOptions<ChatDatabase> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		public static readonly string ConnectionString =
			$"Data Source=..\\OctopusChat\\Server\\Server.Database\\chat.db";
		
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlite(ConnectionString);
		}
	}
}
