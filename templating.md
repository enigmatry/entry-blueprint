## Install the template locally from source (run in solution root)
``dotnet new install .\``

## Build the nuget package (run in solution root)
``dotnet pack Enigmatry.Entry.Blueprint.Template.csproj -c Release``

## Install the template locally from nupkg file
``dotnet new -i "bin\Release\Enigmatry.Entry.Blueprint.Template.1.0.0.nupkg"``

## Install the template from the Azure DevOps nuget feed
``dotnet new install Enigmatry.Entry.Blueprint.Template --add-source https://pkgs.dev.azure.com/enigmatry/b3d80552-b808-495c-9332-82888c12c1bf/_packaging/enigmatry-blueprint-template-feed/nuget/v3/index.json``
If you are not authenticated automatically, add the --interactive argument.

## Install specific version of the template from the Azure DevOps nuget feed
``dotnet new install Enigmatry.Entry.Blueprint.Template::VERSION --add-source https://pkgs.dev.azure.com/enigmatry/b3d80552-b808-495c-9332-82888c12c1bf/_packaging/enigmatry-blueprint-template-feed/nuget/v3/index.json``

where VERSION should be replaced with the specific version you want to install, e.g. 1.0.50

## Deploy a new solution based on the template:
``dotnet new blueprint --name Customer.Project --projectName Customer.Project --appProjectName customer-project --contextName ProjectContext --allow-scripts yes``

## Uninstall the template (when installed from nupkg)
``dotnet new -u Enigmatry.Entry.Blueprint.Template``

## Uninstall the template (when installed from local source)
``dotnet new -u <path to solution e.g. D:\Projects\Enigmatry\enigmatry-entry-blueprint-template>``
