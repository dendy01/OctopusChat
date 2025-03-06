using System.Text.Json;
using Server.Api.Controllers;
using Server.Database;

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
			
			string[] origins = new[]
			{
				//"https://*",
				//"https://f0ea-185-59-102-55.ngrok-free.app",
				//"http://f0ea-185-59-102-55.ngrok-free.app",
				"https://localhost:443",
				"http://localhost:80",
				"https://localhost",
				"http://localhost",
				"null"
			};
			
			builder.Services.AddCors(options =>
			{
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
			

			WebApplication app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}
			
			//using (IServiceScope scope = app.Services.CreateScope())
			//{
				//ChatDatabase database = scope.ServiceProvider.GetService<ChatDatabase>();
				//Seeds seeds = new Seeds(database);
				//seeds.AddMessages();
			//}
			
			app.UseHttpsRedirection();
			app.UseCors("AllowSpecificOrigin");
			app.UseRouting();
			
			app.MapControllers();

			app.Run();
		}
	}
}
