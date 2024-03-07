# back-office-api

En caso de usar una base de datos compatible con entity framework usar el siguiente comando para aplicar las migraciones.

dotnet ef migrations add InitialMigration -p BackOffice.Infrastructure -s BackOffice.API -o Persistence/Migrations