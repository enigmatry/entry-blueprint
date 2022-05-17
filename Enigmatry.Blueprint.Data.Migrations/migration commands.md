Run this from Package Manager Console. 

----------------------------------------------------------------------
-- Powershell commands
----------------------------------------------------------------------
// add migration
Add-Migration -Project Enigmatry.Blueprint.Data.Migrations -StartUpProject Enigmatry.Blueprint.Data.Migrations -Context BlueprintContext -Name TODO

// update database
Update-Database -Project Enigmatry.Blueprint.Data.Migrations -StartUpProject Enigmatry.Blueprint.Data.Migrations -Context BlueprintContext
----------------------------------------------------------------------


----------------------------------------------------------------------
-- dotnet ef commands
----------------------------------------------------------------------
// install global tool
dotnet tool install --global dotnet-ef

// add migration
dotnet-ef migrations add MIGRATION_NAME_HERE --project Enigmatry.Blueprint.Data.Migrations --startup-project Enigmatry.Blueprint.Data.Migrations --context BlueprintContext 

// update database
dotnet-ef database update --project Enigmatry.Blueprint.Data.Migrations --startup-project Enigmatry.Blueprint.Data.Migrations --context BlueprintContext
----------------------------------------------------------------------
