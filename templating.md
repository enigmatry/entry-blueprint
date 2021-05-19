## Install the template locally from source (run in solution root)
``dotnet new -i .\``

## Build the nuget package (run in solution root)
``dotnet pack Enigmatry.Blueprint.Template.csproj -c Release``

## Install the template locally from nupkg file
``dotnet new -i "bin\Release\Enigmatry.Blueprint.Template.1.0.0.nupkg"``

## Install the template from the TeamCity nuget feed
``dotnet new -i Enigmatry.Blueprint.Template::VERSION --nuget-source https://teamcity.enigmatry.com/guestAuth/app/nuget/feed/AspNetCoreAngular_EnigmatryBlueprintTemplate/Feed/v3/index.json``
where VERSION should be replaced with the specific version you want to install, e.g. 1.0.50

## Deploy a new solution based on the template:
``dotnet new blueprint --name Customer.Project --appProjectName customer-project --contextName ProjectContext``

## Uninstall the template (when installed from nupkg)
``dotnet new -u Enigmatry.Blueprint.Template``

## Uninstall the template (when installed from local source)
``dotnet new -u <path to solution e.g. D:\Projects\Enigmatry\enigmatry-blueprint-template>``
