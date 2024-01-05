# Theming Setup

- [Overview](#overview)
- [Theme](#theme)
  - [Define custom theme](#define-custom-theme)
  - [Fonts](#fonts)
  - [Adding Material classes](#adding-material-classes)
- [Default values table](#default-values-table)
- [Theming Configuration Guide](#theming-configuration-guide)

## Overview

The entry theme allows you to customize the styles of your application according to your needs. How to set up theming in your project, can be found in further sections.

## Theme

`_general.scss` file inside `styles/partials/vendors/libraries/entry/` folder is the place where it is located theme-related style configurations. The file contains imports for generator files for configuring theming, a Sass map in which you can define styles according to your needs, and a generator generates-from mixin, which passes a custom theme ( set of style configurations ) as an argument. If you don't change anything from the styles then the styling from the default theme will be applied.

### Define custom theme

Inside the `$custom-theme` map, you can define custom styles to suit specific preferences and needs.

`$custom-theme` map includes 3 main sections:

1) **general**
- it contains styling for colors, density, typography, fonts, spacing, inputs, buttons
2) **tables**
- it contains styling for cells, rows, content
3) **dialog**
- it contains font size for the dialog title

For more info about all these properties you can read [here](https://github.com/enigmatry/entry-angular-building-blocks/blob/master/libs/entry-components/configure-theming.md#1-custom-configuration).

### Fonts

There are 2 possibilities for using fonts in theming:

1. Default Roboto font

The theme comes with the default **Roboto** font family. Configuration is placed inside the `angular.json` file. The font is imported from the library's node modules.

2. Custom fonts

If you need a custom fonts, you can do it in the following way:

1) put custom font files in the `assets/fonts/` folder of the project
2) create typography.scss file inside the `partials/core/fonts` folder
3) use the `@font-face` rule in `_typography.scss` file to define custom fonts with an absolute path

```scss
@font-face {
  font: {
    family: 'Helvetica';
    weight: 400;
  }
  src: url('/assets/fonts/Helvetica.woff2') format('woff2'), url('/assets/fonts/Helvetica.woff') format('woff');
}
```
4) remove the configuration from the assets part inside the `angular.json` file:

```json
"assets": [
  {
    "glob": "**/*",
    "input": "./node_modules/@enigmatry/entry-components/assets/",
    "output": "/assets/"
  }
]
```
### Adding Material classes

Since Angular Material uses different typography levels, to be able to read values from the theme it's needed to add those typography-level cases to corresponding native elements. Check the full list [here](https://material.angular.io/guide/typography#typography-levels).

We have added `.mat-body` class to the **body** element in `index.html` file in app root.

We're overriding Angular Material, so it is important to add CSS classes for mixin that emit styles for native header elements. In another case, if don't provide those classes for custom components, typography configurations will not be applied.

 ```html
<!-- Example of adding material class -->
<section>
  <h1 class="mat-headline-1">Main header</h1>
</section>
```
Based on the project needs, choose one of two configuration options. By default, default-theme will be applied if custom-theme object is empty. If fonts are not specified, body fonts will be used for everything.

More about typography configurations can be found [here](https://github.com/enigmatry/entry-angular-building-blocks/blob/master/libs/entry-components/configure-theming.md#3-fonts).

## Default values table

The table below outlines the default values for various configuration properties. If a specific value is not defined in your project's configuration, the default value from the table will be applied.

| Category         | Property                        | Default Value                       |
|------------------|---------------------------------|-------------------------------------|
| **general**      | density                         | 0                                   |
|                  | primary-theme                   | null                                |
|                  | accent-theme                    | null                                |
|                  | primary                         | #2581C4                             |
|                  | accent                          | #EA518D                             |
|                  | font                            | #323232                             |
|                  | disabled/foreground             | rgb(0 0 0 / .38)                    |
|                  | disabled/background             | rgb(0 0 0 / .12)                    |
|                  | typography                      | null                                |
|                  | family                          | ''                                  |
|                  | size                            | 0                                   |
|                  | default                         | 15px                                |
|                  | inputs/background               | null                                |
|                  | icon-size                       | 48px                                |
| **tables**       | edge-gap                        | 4px                                 |
|                  | padding                         | null                                |
|                  | selected-color                  | #FFF                                |
|                  | disabled-color                  | #F5F5F5                             |
|                  | odd-even-row                    | odd                                 |
|                  | odd-even-background             | #F0F0F0                             |
|                  | font-size                       | 13px                                |
|                  | font-weight                     | 500                                 |
| **dialogs**      | size                            | 20px                                |


## Theming Configuration Guide
For detailed information and a deeper understanding of theming concepts and customization possibilities, visit our comprehensive guide [here](https://github.com/enigmatry/entry-angular-building-blocks/blob/master/libs/entry-components/configure-theming.md#theming-configuration-guide).
