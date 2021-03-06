# Step by Step Tutorial

1. Create new blank Solution
1. Add new project API ASP.NET Core
   1. Name: **API.Cinema**
   1. Add NuGet package: **Microsoft.EntityFrameworkCore.Design**
1. Add new project 
   1.  Name: **Data.Cinema**
   1. Add NuGet package: **Npgsql.EntityFrameworkCore.PostgreSQL**
1. Add "Data.Cinema to main project "API.Cinema" references
1. Install EF Core CLI globally (optional):
   ```
   dotnet tool install --global dotnet-ef
   ```
   update it:
   ```
   dotnet tool update --global dotnet-ef
   ```
1. Build code model based on database structure:
   ```
   dotnet ef dbcontext scaffold 
      "Server=localhost;Port=5432;Database=cinema;" 
      Npgsql.EntityFrameworkCore.PostgreSQL 
      --project "Data.Cinema/" 
      --startup-project "API.Cinema/"
   ```

## Migrations (initial)

1. Add `Microsoft.EntityFrameworkCore.Design` to API.Cinema (statup) project
   ```
   cd API.Cinema
   dotnet add package Microsoft.EntityFrameworkCore.Design
   ```
1. Create Initial migration (CLI)
   ```
   cd ..\Data.Cinema
   dotnet ef migrations add InitialCreate -s ../API.Cinema
   ```
1. Generate SQL daabse script (CLI)
   ```
   dotnet ef migrations script -s ../API.Cinema
   ```

## Migrations (upgrads)

1. Add new object structures (classes / properties) for entitites
1. Add migration
   ```
   dotnet ef migrations add AddMovieLauchDate -s ../API.Cinema 
   ```
1. Upgrade database
   ```
   adotnet ef database update -s ../API.Cinema
   ```
    