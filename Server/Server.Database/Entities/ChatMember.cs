namespace Server.Database.Entities
{
	public class ChatMember
	{
		public long Id { get; set; }
		public long UserId { get; set; }
		public User User { get; set; }
		public long ChatId { get; set; }
		public Chat Chat { get; set; }
	}
}