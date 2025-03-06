namespace Server.Database.Entities;

public class Chat
{
	public long Id { get; set; }
	public List<User> Members { get; set; }
}