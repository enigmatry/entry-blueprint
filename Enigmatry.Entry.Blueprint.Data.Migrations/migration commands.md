# Entity framework migration commands

Run these commands from Package Manager Console.

## Powershell commands

### Add migration

``` powershell
Add-Migration -Project Enigmatry.Entry.Blueprint.Data.Migrations -StartUpProject Enigmatry.Entry.Blueprint.Data.Migrations -Context BlueprintContext -Name MIGRATION_NAME_HERE
```

### Update database

``` powershell
Update-Database -Project Enigmatry.Entry.Blueprint.Data.Migrations -StartUpProject Enigmatry.Entry.Blueprint.Data.Migrations -Context BlueprintContext
```

### Remove migration

``` powershell
Remove-Migration -Project Enigmatry.Entry.Blueprint.Data.Migrations -StartUpProject Enigmatry.Entry.Blueprint.Data.Migrations
```

### Revert to a specific migration (discard all migrations created after the specified one)

``` powershell
Update-Database -Project Enigmatry.Entry.Blueprint.Data.Migrations -StartUpProject Enigmatry.Entry.Blueprint.Data.Migrations -Context BlueprintContext THE-LAST-GOOD-MIGRATION-NAME
```

## Command line commands

### Install global tool

``` shell
dotnet tool install --global dotnet-ef
```

### Update global tool to latest version

``` shell
dotnet tool update --global dotnet-ef
```

### Add migration

``` shell
dotnet-ef migrations add MIGRATION_NAME_HERE --project Enigmatry.Entry.Blueprint.Data.Migrations --startup-project Enigmatry.Entry.Blueprint.Data.Migrations --context BlueprintContext
```

### Update database

``` shell
dotnet-ef database update --project Enigmatry.Entry.Blueprint.Data.Migrations --startup-project Enigmatry.Entry.Blueprint.Data.Migrations --context BlueprintContext
```

### Remove migration

``` shell
dotnet-ef migration remove --project Enigmatry.Entry.Blueprint.Data.Migrations --startup-project Enigmatry.Entry.Blueprint.Data.Migrations
```

### Revert to a specific migration (discard all migrations created after the specified one)

``` shell
dotnet-ef database update --project Enigmatry.Entry.Blueprint.Data.Migrations --startup-project Enigmatry.Entry.Blueprint.Data.Migrations THE-LAST-GOOD-MIGRATION-NAME
```
