# Enigmatry Blueprint Template project

* [Starting the app](#starting-the-app)
  * [1. Starting the API](#1-starting-the-api)
  * [2. Configuring the App](#2-configuring-the-app)
  * [3. Starting the App](#3-starting-the-app)
* [Code generation](#code-generation)
  * [1. Installing code generator tool](#1-installing-code-generator-tool)
  * [2. Configuring the code to be generated](#2-configuring-the-code-to-be-generated)
  * [3. Generating code](#3-generating-code)


## Starting the app

### 1. Starting the API

Start the API by setting `Enigmatry.Blueprint.Api` as start up project and by starting the solution.

### 2. Configuring the App

This should be done just the first time!

Configure Azure DevOps npm feed by executing the following commands: `npm install -g vsts-npm-auth` & `vsts-npm-auth -config .npmrc`. Authenticate, if asked, using Enigmatry account.

Execute `npm i` to install all npm packages (repeat this step if some of the packages is updated/added/removed).

### 3. Starting the App

To start the app execute command `npm start`, which will build & run the app on <http://localhost:4200/>.

## Code generation

### 1. Installing code generator tool

To install the tool run npm script `npm run codegen:install`. This will install the tool globally and it needs to be executed only once.

To update the tool run `npm run codegen:update`. This npm script should be executed each time new version of the tool is out.

### 2. Configuring the code to be generated

All code generation configurations are located in `Enigmatry.Blueprint.CodeGeneration.Setup` project. For configurations to be visible for the tool, the project must be re-built each time there are changes in the configuration definitions.

### 3. Generating code

To generate the code from configurations, execute npm script `npm run codegen`. This command will run the tool which will generate configured code inside features modules.

