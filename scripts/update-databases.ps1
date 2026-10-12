Write-Host "Updating AppDbContext database"
dotnet-ef database update --project ../Enigmatry.Entry.Blueprint.Data.Migrations --startup-project ../Enigmatry.Entry.Blueprint.Data.Migrations --context AppDbContext
