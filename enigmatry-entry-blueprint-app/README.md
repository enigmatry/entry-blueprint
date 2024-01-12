# Enigmatry Blueprint Template project

* [Starting the app](#starting-the-app)
  * [1. Starting the API](#1-starting-the-api)
  * [2. Configuring the App](#2-configuring-the-app)
  * [3. Starting the App](#3-starting-the-app)
* [Theming Setup](#theming-setup)

## Starting the app

### 1. Starting the API

Start the API by setting `Enigmatry.Entry.Blueprint.Api` as start up project and by starting the solution.

### 2. Configuring the App

This should be done just the first time!

Configure Azure DevOps npm feed by executing the following commands: `npm install -g vsts-npm-auth` & `vsts-npm-auth -config .npmrc`. Authenticate, if asked, using Enigmatry account.

Execute `npm i` to install all npm packages (repeat this step if some of the packages is updated/added/removed).

### 3. Starting the App

To start the app execute command `npm start`, which will build & run the app on <http://localhost:4200/>.

## Theming Setup
For detailed instructions to set up theming refer to the
[Theming Setup](theming-setup.md).