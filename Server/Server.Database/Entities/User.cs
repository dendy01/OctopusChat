using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Server.Database.Entities;

public class User : IdentityUser<long>
{
	//public long Id { get; set; }
	
	// [Column(TypeName = "varchar(64)")]
	// public string Name { get; set; }

	[Column(TypeName = "varchar(64)")]
	public string Password { get; set; }
}

public class Role : IdentityRole<long>
{
}