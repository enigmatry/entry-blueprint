# Theming Setup

- [Overview](#overview)
- [Theme](#theme)
  - [Define custom theme](#define-custom-theme)
  - [Fonts](#fonts)
  - [Adding Material classes](#adding-material-classes)
- [Default values table](#default-values-table)
- [Theming Configuration Guide](#theming-configuration-guide)

## Overview

The Entry theme works hand-in-hand with Angular Material. It allows you to configure only certain things for simplicity and quick setup. How to set up the theme in your project, can be found in further sections.

## Theme

The theme scss file needs to contain imports for generator files for configuring theming, a Sass map in which you can define styles according to your needs, and a generator generate-from mixin, which passes a custom theme ( set of style configurations ) as an argument. If you don't change anything from the styles then the styling from the default theme will be applied.

### Define custom theme

Inside the `$custom-theme` map, you can define custom styles to suit specific preferences and needs.
If a specific value is not defined in your project's custom theme, the value from the default theme will be applied.

`$custom-theme` map includes 3 main sections:

1) **general**
- it contains styling for colors, density, typography, fonts, spacing, inputs, buttons
2) **tables**
- it contains styling for cells, rows, content
3) **dialog**
- it contains font size for the dialog title

For more information about all these properties you can read [here](https://github.com/enigmatry/entry-angular-building-blocks/blob/master/libs/entry-components/configure-theming.md#1-custom-configuration).

### Fonts

There are 2 possibilities for using fonts in theming:

1. Default Roboto font

The theme comes with the default **Roboto** font family.
You should add inside angular.json file the following stuff for the default font to work:

```json
"assets": [
  {
    "glob": "**/*",
    "input": "./node_modules/@enigmatry/entry-components/assets/",
    "output": "/assets/"
  }
]
```

2. Custom fonts

If you need custom fonts, you can do it in the following way:
- put custom font files in the `assets/fonts/` folder of the project
- i.e use @font-face rule to define custom fonts in your scss file in the following way:

```scss
@font-face {
  font: {
    family: 'Helvetica';
    weight: 400;
  }
  src: url('/assets/fonts/Helvetica.woff2') format('woff2'), url('/assets/fonts/Helvetica.woff') format('woff');
}
```

### Adding Material classes

Since Angular Material uses different typography levels, to be able to read values from the theme it's needed to add those typography-level cases to corresponding native elements. Check the full list [here](https://material.angular.io/guide/typography#typography-levels).

You should add `.mat-body` class to the **body** element in `index.html` file to achieve the expected look and feel.

It is important to add CSS classes for mixin that emit styles for native header elements. In another case, if don't provide those classes for custom components, typography configurations will not be applied.

 ```html
<!-- Example of adding material class -->
<section>
  <h1 class="mat-headline-1">Main header</h1>
</section>
```
Based on the project needs, choose one of two configuration options. By default, default-theme will be applied if custom-theme object is empty. If fonts are not specified, body fonts will be used for everything.

More about typography configurations can be found [here](https://github.com/enigmatry/entry-angular-building-blocks/blob/master/libs/entry-components/configure-theming.md#3-fonts).

## Default values table

The table below outlines the default values for various configuration properties.

| Category         | Property                        | Default Value                       |
|------------------|---------------------------------|-------------------------------------|
| **general**      | density                         | 0                                   |
|                  | colors/primary-theme            | null                                |
|                  | colors/accent-theme             | null                                |
|                  | colors/primary                  | #2581C4                             |
|                  | colors/accent                   | #EA518D                             |
|                  | colors/font                     | #323232                             |
|                  | disabled/foreground             | rgb(0 0 0 / .38)                    |
|                  | disabled/background             | rgb(0 0 0 / .12)                    |
|                  | typography                      | null                                |
|                  | fonts/family                    | ''                                  |
|                  | fonts/size                      | 0                                   |
|                  | fonts/letter-spacing            | null                                |
|                  | spacing/default                 | 15px                                |
|                  | inputs/background               | null                                |
|                  | buttons/icon-size               | 48px                                |
| **tables**       | cells/edge-gap                  | 4px                                 |
|                  | cells/padding                   | null                                |
|                  | rows/selected-color             | #FFF                                |
|                  | rows/disabled-color             | #F5F5F5                             |
|                  | rows/odd-even-row               | odd                                 |
|                  | rows/odd-even-background        | #F0F0F0                             |
|                  | no-result/font-size             | 13px                                |
|                  | no-result/font-weight           | 500                                 |
| **dialogs**      | title/size                      | 20px                                |


## Theming Configuration Guide
For detailed information and a deeper understanding of theming concepts and customization possibilities, visit our comprehensive guide [here](https://github.com/enigmatry/entry-angular-building-blocks/blob/master/libs/entry-components/configure-theming.md#theming-configuration-guide).
