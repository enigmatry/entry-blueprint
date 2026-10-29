$migrationsProject = Join-Path $PSScriptRoot "../Enigmatry.Entry.Blueprint.Data.Migrations"

Write-Host "Adding migration to AppDbContext"
$migrationName = Read-Host "Enter migration name (or 'exit' to quit)"

#Write-Host "You have entered: $migrationName"

# Optionally, include any logic you want to perform with the user input
# For example, you can check if the input matches certain criteria
if ($migrationName -eq "exit")
{
    Write-Host "Exiting the script."
    exit
}
else
{
    dotnet-ef migrations add $migrationName --project $migrationsProject --startup-project $migrationsProject --context AppDbContext
}