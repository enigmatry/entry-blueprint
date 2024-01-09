## Install the template locally from source (run in solution root)
``dotnet new install .\``

## Build the nuget package (run in solution root)
``dotnet pack Enigmatry.Entry.Blueprint.Template.csproj -c Release``

## Install the template locally from nupkg file
``dotnet new -i "bin\Release\Enigmatry.Entry.Blueprint.Template.1.0.0.nupkg"``

## Install the template from NuGet.org
``dotnet new install Enigmatry.Entry.Blueprint.Template``
If you are not authenticated automatically, add the --interactive argument.

## Install specific version of the template from NuGet.org
``dotnet new install Enigmatry.Entry.Blueprint.Template::VERSION``

where VERSION should be replaced with the specific version you want to install, e.g. 2.0.1

## Deploy a new solution based on the template:
``dotnet new blueprint --name Customer.Project --projectName Customer.Project --appProjectName customer-project --friendlyName "Customer Project" --allow-scripts yes``

## Uninstall the template (when installed from nupkg)
``dotnet new -u Enigmatry.Entry.Blueprint.Template``

## Uninstall the template (when installed from local source)
``dotnet new -u <path to solution e.g. D:\Projects\Enigmatry\enigmatry-entry-blueprint-template>``
