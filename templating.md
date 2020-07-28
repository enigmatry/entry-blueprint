## Install the template locally from source (run in solution root)
dotnet new -i .\

## Build the nuget package (run in solution root)
dotnet pack Enigmatry.Blueprint.Template.csproj

## Install the template locally from nupkg file
dotnet new -i "bin\Debug\Enigmatry.Blueprint.Template.1.0.0.nupkg"

## Deploy a new solution based on the template:
dotnet new blueprint --name Edwin.Test --appProjectName edwin-test-app --contextName EdwinContext

## uninstall the template (when installed from nupkg)
dotnet new -u Enigmatry.Blueprint.Template
