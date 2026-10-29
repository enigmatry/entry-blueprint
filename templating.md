## Install the template locally from source (run in solution root)
``dotnet new install .\``

## Build the nuget package (run in solution root)
``dotnet pack Enigmatry.Entry.Blueprint.Template.csproj -c Release``

## Install the template locally from nupkg file
``dotnet new install "bin\Release\Enigmatry.Entry.Blueprint.Template.<VERSION>.nupkg"``

## Install the template from NuGet.org
``dotnet new install Enigmatry.Entry.Blueprint.Template``
If you are not authenticated automatically, add the --interactive argument.

## Install specific version of the template from NuGet.org
``dotnet new install Enigmatry.Entry.Blueprint.Template::VERSION``

where VERSION should be replaced with the specific version you want to install, e.g. 2.0.1

## Deploy a new solution based on the template:
``dotnet new blueprint --name Customer.Project --projectName Customer.Project --appProjectName customer-project --friendlyName "Customer Project" --allow-scripts yes``

`dotnet new blueprint -h` lists all options:

| Option | Description |
|---|---|
| `--projectName` | Dotted .NET name, e.g. `Customer.Project` |
| `--appProjectName` | Kebab-cased name used for the Angular app, database and Azure resources, e.g. `customer-project` |
| `--friendlyName` | Display name, e.g. `"Customer Project"` |
| `--no-restore` | Skips the restore after creation |
| `--no-git` | Skips `git init` and the initial commit |
| `--apiHttpPort`, `--apiHttpsPort` | Ports of the API in `launchSettings.json` (generated when omitted) |
| `--appHostHttpPort`, `--appHostHttpsPort`, `--appHostOtlpHttpPort`, `--appHostOtlpHttpsPort`, `--appHostResourceHttpPort`, `--appHostResourceHttpsPort` | Ports of the Aspire AppHost in `launchSettings.json` (generated when omitted) |

## Uninstall the template (when installed from nupkg)
``dotnet new uninstall Enigmatry.Entry.Blueprint.Template``

## Uninstall the template (when installed from local source)
``dotnet new uninstall <path to the template's solution root>``
