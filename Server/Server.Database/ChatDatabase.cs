using Microsoft.EntityFrameworkCore;
using Server.Database.Entities;

namespace Server.Database
{
	public sealed class ChatDatabase : DbContext
	{
		public DbSet<Message> Messages { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Chat> Chats { get; set; }

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
