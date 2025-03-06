using System.Text.Json;

namespace Server.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			builder.Services.AddHttpsRedirection(options =>
			{
				options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
				options.HttpsPort = 443;
			});
			
			string[] origins = new[]
			{
				//"https://*",
				"https://7c10-185-59-102-55.ngrok-free.app",
				"http://7c10-185-59-102-55.ngrok-free.app",
				"https://localhost:443",
				"http://localhost:80",
			};
			
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigin",
					b =>
					{
						b.WithOrigins(origins)
							.AllowAnyMethod()
							.AllowAnyHeader();
					});
			});
			
			builder.Services.AddOpenApi();
			builder.Services.AddControllers()
				.AddJsonOptions(options =>
					options.JsonSerializerOptions.PropertyNamingPolicy = null);

			WebApplication app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}
			
			
			app.UseHttpsRedirection();
			app.UseCors("AllowSpecificOrigin");
			app.UseRouting();
			
			app.MapControllers();

			app.Run();
		}
	}
}
