dotnet ef migrations add InitialDatabase --project ../Server.Database --startup-project ../Server.Api
dotnet ef database update --project ../Server.Database --startup-project ../Server.Api