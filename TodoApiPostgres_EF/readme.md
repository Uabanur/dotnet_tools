# MySql server example with EntityFramework Core

To create migration based on context:

- dotnet tool install --global dotnet-ef
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet ef migrations add InitialCreate
- dotnet ef database update   # Migrate on startup in code


from: https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli