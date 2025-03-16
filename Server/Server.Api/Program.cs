using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Server.Api.Controllers;
using Server.Database;
using Server.Database.Entities;

namespace Server.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			// builder.Services.AddHttpsRedirection(options =>
			// {
			// 	//options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
			// 	options.HttpsPort = 443;
			// });

			builder.Services.AddSignalR();
			
			string[] origins = new[]
			{
				//"https://*",
				//"https://f0ea-185-59-102-55.ngrok-free.app",
				//"http://f0ea-185-59-102-55.ngrok-free.app",
				// "https://localhost:443",
				// "http://localhost:80",
				// "https://localhost",
				// "http://localhost",
				// "https://localhost:5173",
				"http://localhost:5173",
				"null"
			};
			//var now = DateTime.Now;
			builder.Services.AddCors(options =>
			{
				//options.AddPolicy("AllowAll",
				//	builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
				
				options.AddPolicy("AllowSpecificOrigin",
					b =>
					{
						b.WithOrigins(origins)
							.AllowAnyMethod()
							.AllowAnyHeader()
							.AllowCredentials();
					});
			});
			
			builder.Services.AddOpenApi();
			builder.Services.AddControllers()
				.AddJsonOptions(options =>
					options.JsonSerializerOptions.PropertyNamingPolicy = null);

			builder.Services.AddSqlite<ChatDatabase>(ChatDatabase.ConnectionString);

			builder.Services.AddIdentity<User, IdentityRole<long>>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ChatDatabase>()
				.AddDefaultTokenProviders();
			
			builder.Services.Configure<AccountConfiguration>(
				builder.Configuration.GetSection(nameof(AccountConfiguration)));

			builder.Services.AddSingleton<RoomService>();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;

				AccountConfiguration accountConfiguration = builder.Configuration
					.GetSection(nameof(AccountConfiguration))
					.Get<AccountConfiguration>();
				
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = accountConfiguration.ValidIssuer,
					ValidAudience = accountConfiguration.ValidAudience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accountConfiguration.JwtKey)),
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};
			});
			
			
			WebApplication app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}
			
			// using (IServiceScope scope = app.Services.CreateScope())
			// {
			// 	ChatDatabase database = scope.ServiceProvider.GetService<ChatDatabase>();
			// 	Seeds seeds = new Seeds(database);
			// 	seeds.AddMessages();
			// }
			
			app.UseMiddleware<JwtAuthorizationMiddleware>();
			
			app.UseRouting();
			
			app.UseCors("AllowSpecificOrigin");
			//app.UseCors("AllowAll");

			app.UseAuthorization();
			app.UseAuthentication();
			
			app.UseHttpsRedirection();
			
			app.MapControllers();
			app.MapHub<ChatHub>("/chathub");

			app.Run();
		}
	}
}
