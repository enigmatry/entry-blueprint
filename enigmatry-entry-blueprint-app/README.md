# Enigmatry Blueprint Template project

* [Starting the app](#starting-the-app)
  * [1. Starting the API](#1-starting-the-api)
  * [2. Configuring the App](#2-configuring-the-app)
  * [3. Starting the App](#3-starting-the-app)
* [Code generation](#code-generation)
  * [1. Installing code generator tool](#1-installing-code-generator-tool)
  * [2. Configuring the code to be generated](#2-configuring-the-code-to-be-generated)
  * [3. Generating code](#3-generating-code)
* [Code generator configuration](#code-generator-configuration)
  * [Custom validators](#custom-validators)
  * [Form fields conditional visibility](#form-fields-conditional-visibility)
* [Theming Customization](#theming-customization)

## Starting the app

### 1. Starting the API

Start the API by setting `Enigmatry.Entry.Blueprint.Api` as start up project and by starting the solution.

### 2. Configuring the App

This should be done just the first time!

Configure Azure DevOps npm feed by executing the following commands: `npm install -g vsts-npm-auth` & `vsts-npm-auth -config .npmrc`. Authenticate, if asked, using Enigmatry account.

Execute `npm i` to install all npm packages (repeat this step if some of the packages is updated/added/removed).

### 3. Starting the App

To start the app execute command `npm start`, which will build & run the app on <http://localhost:4200/>.

## Code generation

### 1. Installing code generator tool

Install [Azure artifacts credential provider](https://github.com/microsoft/artifacts-credprovider#setup)

`iex "& { $(irm https://aka.ms/install-artifacts-credprovider.ps1) } -AddNetfx"`

To install the tool run npm script `npm run codegen:install`. This will install the tool globally and it needs to be executed only once.

To update the tool run `npm run codegen:update`. This npm script should be executed each time new version of the tool is out.

### 2. Configuring the code to be generated

All code generation configurations are located in `Enigmatry.Entry.Blueprint.CodeGeneration.Setup` project. For configurations to be visible for the tool, the project must be re-built each time there are changes in the configuration definitions. More details in [Code generator configuration](#code-generator-configuration)

### 3. Generating code

To generate the code from configurations, execute npm script `npm run codegen:run`. This command will run the tool which will generate configured code inside features modules.

## Code generator configuration

### Custom validators

Custom validator can be applied in `IFormComponentConfiguration` configurations on FormControl level. They are configured via `WithValidator()` setter that accepts validator name and optional validator trigger type (default is `OnBlur`). Validator name matches validator name on client side. Validator trigger determines form event on which validator will trigger (`OnChange` & `OnSubmit` are other supported options).

Generated code expects validators to be configured in the following default location `shared/validators/custom-validators`. This location can be customized via code generator console command flag `--validators-path`.

`custom-validator` file must export 2 things:

* `customValidatorsFactory`, holds definitions of the validators & validation messages
* `CustomValidatorsService`, a wrapper service used to inject other dependencies in validators

`customValidatorsFactory` is function that receives `CustomValidatorsService` as input and have result in following format:

```ts
{
  validationMessages: [
    { name: '[VALIDATOR_NAME]', message: '[VALIDATION_MESSAGE]' },
    // ...
  ],
  validators: [
    {
        name: '[VALIDATOR_NAME]',
        validation: (control: FormControl, service: CustomValidatorsService)
          : Promise<{ VALIDATOR_NAME: boolean } | null> =>
          { /* validator logic goes here */ }
    }
  ]
}
```

`[VALIDATOR_NAME]` placeholder must match configured name of the validator (in camel-case, 'isEmailUnique').

`CustomValidatorsService` is a service used to inject dependencies into validators. For example, this service can expose API clients methods to be used in validators to hit the API.

### Form fields conditional visibility

To show/hide form fields based on condition, we use `fieldsHideExpressions` form input. This input accepts configuration dictionary of type `IHideExpressionDictionary<IFromModel>`, which contains conditions for selected form fields. Example:

```ts
const hideExpressionsDictionary: IHideExpressionDictionary<IFromModel> = {};
hideExpressionsDictionary.name =
  (model: IFromModel)): boolean {
    return model.showName === true;
  }
```

In this example, we defined hide-expression condition for form field `name` to be visible only when from field `showName` is set to `true`. We can add expressions for other form fields.

After creating the dictionary, we pass it to form `fieldsHideExpressions` input:

```html
<app-g-some-from [fieldsHideExpressions]="hideExpressionsDictionary">
</app-g-some-from>
```

## Theming Customization
Blueprint comes with a pre-configured theme. If you need to customize or alter theme, please refer to detailed documentation on 
[Theming Customization](theming-setup.md).