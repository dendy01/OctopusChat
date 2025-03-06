using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities;

public class User
{
	public long Id { get; set; }
	
	[Column(TypeName = "varchar(64)")]
	public string Name { get; set; }
}