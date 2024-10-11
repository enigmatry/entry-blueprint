Write-Host "Updating App db"

dotnet-ef database update --project ../Enigmatry.Entry.Blueprint.Data.Migrations --startup-project ../Enigmatry.Entry.Blueprint.Data.Migrations --context AppDbContext