$migrationsProject = Join-Path $PSScriptRoot "../Enigmatry.Entry.Blueprint.Data.Migrations"

Write-Host "Updating AppDbContext database"
dotnet-ef database update --project $migrationsProject --startup-project $migrationsProject --context AppDbContext
