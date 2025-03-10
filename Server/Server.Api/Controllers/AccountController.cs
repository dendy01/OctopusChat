using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Database.Entities;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Server.Api.Controllers
{
	public class LoginModel
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
	public class RegisterModel
	{
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
	
	public class JwtAuthorizationMiddleware
	{
		private readonly RequestDelegate _next;
		private const string SecretKey = "527c1d7e0439c015b270b9db9a8c8e37be317cd0319231b6a23805c08971888c";

		public JwtAuthorizationMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        
			if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
			{
				var token = authorizationHeader.Substring("Bearer ".Length).Trim();
				var principal = ValidateToken(token);
				if (principal != null)
				{
					context.User = principal; // Устанавливаем пользователя в HttpContext
				}
			}

			await _next(context);
		}

		private ClaimsPrincipal ValidateToken(string token)
		{
			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.UTF8.GetBytes(SecretKey);

				var parameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};

				return tokenHandler.ValidateToken(token, parameters, out _);
			}
			catch
			{
				return null;
			}
		}
	}

	public class AccountConfiguration
	{
		public string JwtKey { get; set; }
		public string ValidIssuer { get; set; }
		public string ValidAudience { get; set; }
	}

	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly AccountConfiguration _accountConfiguration;
		
		public AccountController(UserManager<User> userManager,
			SignInManager<User> signInManager,
			IOptions<AccountConfiguration> accountConfiguration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_accountConfiguration = accountConfiguration.Value;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			if (model is null || model.Password != model.ConfirmPassword)
			{
				return BadRequest("The passwords did not match.");
			}

			User user = new() { UserName = model.UserName, Email = model.Email };
			IdentityResult result = await _userManager.CreateAsync(user, model.Password);
			
			if (result.Succeeded)
			{
				return Ok("User registered successfully.");
			}

			return BadRequest(result.Errors);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			if (model is null || String.IsNullOrEmpty(model.UserName) || String.IsNullOrEmpty(model.Password))
			{
				return BadRequest("UserName and Password are required.");
			}

			User user = await _userManager.FindByNameAsync(model.UserName);

			if (user is null)
			{
				return Unauthorized("Invalid UserName and Password.");
			}

			// await _signInManager.SignInAsync(user, isPersistent: false);
			
			//bool isVerifyPassword = await _userManager.CheckPasswordAsync(user, model.Password);
			
			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

			if (result.Succeeded)
			{
				string token = GenerateJwtToken(user);

				return Ok(new { token, currentUserId = user.Id });
			}

			return Unauthorized("Invalid UserName and Password.");
		}

		private string GenerateJwtToken(User user)
		{
			Claim[] claims =
			{
				new(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new(ClaimTypes.Name, user.UserName!),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			/// For generate key with js see https://dev.to/tkirwa/generate-a-random-jwt-secret-key-39j4
			/// Generate: https://jwtsecret.com/generate
			
			byte[] keyBytes = Encoding.UTF8.GetBytes(_accountConfiguration.JwtKey);;
			
			SymmetricSecurityKey key = new(keyBytes);
			SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

			//AuthenticationMiddleware
			
			JwtSecurityToken token = new(
				issuer: "OctopusChat.com",
				audience: "OctopusChat.com",
				claims: claims,
				expires: DateTime.Now.AddHours(3),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}