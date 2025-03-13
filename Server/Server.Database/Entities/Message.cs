using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Database.Entities;

public class Message
{
	public long Id { get; set; }
	
	[Column(TypeName = "varchar(512)")]
	public string Text { get; set; }
	public DateTime CreatedDateTime { get; set; }

	public long ChatId { get; set; }
	[JsonIgnore]
	public Chat Chat { get; set; }
	
	public long UserId { get; set; }
	[JsonIgnore]
	public User User { get; set; }
}