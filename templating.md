## Install the template locally from source (run in solution root)
``dotnet new -i .\``

## Build the nuget package (run in solution root)
``dotnet pack Enigmatry.Blueprint.Template.csproj -c Release``

## Install the template locally from nupkg file
``dotnet new -i "bin\Debug\Enigmatry.Blueprint.Template.1.0.0.nupkg"``

## Install the template from the TeamCity nuget feed
1. Add the Enigmatry Blueprint feed as Source, with your Enigmatry Username and password:
``dotnet nuget add source http://teamcity.enigmatry.local/guestAuth/app/nuget/feed/AspNetCoreAngular_EnigmatryBlueprintTemplate/Feed/v3/index.json --name "Enigmatry Blueprint Template Feed"``
1. Install from the nuget source you just created:
``dotnet new -i Enigmatry.Blueprint.Template``

## Deploy a new solution based on the template:
``dotnet new blueprint --name Customer.Project --appProjectName customer-project --contextName ProjectContext``

## uninstall the template (when installed from nupkg)
``dotnet new -u Enigmatry.Blueprint.Template``
