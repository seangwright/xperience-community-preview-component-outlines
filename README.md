# Xperience Community: Preview Component Outlines

Enables outlines of Page Builder components in Preview mode for a Page.

This can help marketers and content managers visualize how various Page Builder components are composed on a page without the design limitations of the Page Builder edit mode.

![Outlines and labels of Page Builder components in Preview mode](https://raw.githubusercontent.com/seangwright/xperience-community-preview-component-outlines/main/images/outlines.gif)

[![GitHub Actions CI: Build](https://github.com/seangwright/xperience-community-preview-component-outlines/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/seangwright/xperience-community-preview-component-outlines/actions/workflows/ci.yml)

[![Publish Packages to NuGet](https://github.com/seangwright/xperience-community-preview-component-outlines/actions/workflows/publish.yml/badge.svg?branch=main)](https://github.com/seangwright/xperience-community-preview-component-outlines/actions/workflows/publish.yml)

## Packages

### PreviewComponentOutlines

[![NuGet Package](https://img.shields.io/nuget/v/XperienceCommunity.PreviewComponentOutlines.svg)](https://www.nuget.org/packages/XperienceCommunity.PreviewComponentOutlines)

_Requirements_: [Xperience by Kentico](https://docs.xperience.io/xp) >= v25.0.0

## Setup

Install the `XperienceCommunity.PreviewComponentOutlines` NuGet package in your ASP.NET Core application:

```bash
dotnet add package XperienceCommunity.PreviewComponentOutlines
```

In your `Program.cs` add the following line where the rest of your services are configured:

```csharp
builder.Services.AddPreviewComponentOutlines();
```

If you want to configure the styles of the outlines and labels, use the method overload:

```csharp
builder.Services.AddPreviewComponentOutlines(o =>
{
    o.LabelFontColor = "#3a3a3a";
});
```

In your `_ViewImports.cshtml` add the following line to make the library's tag helper available in your Razor views:

```razor
@addTagHelper *, XperienceCommunity.PreviewComponentOutlines
```

In each Page Builder component you would like to have an outline and label, add the following tag helper to the most top-level HTML element of the component's view, where `Component Widget|Section` is the name of the component:

```html
<div xpc-preview-outline="Component Widget|Section"></div>
```

**Note**: The name of the component must end in "Section" or "Widget" to ensure the styles are applied correctly.

Example:

```html
<!-- _SingleColumnSection.cshtml -->

<section xpc-preview-outline="Single Column Section">
  <!-- ... other markup -->
</section>
```

## Contributions

If you discover a problem, please [open an issue](https://github.com/seangwright/xperience-community-preview-component-outlines/issues/new).

If you would like contribute to the code or documentation, please [open a pull request](https://github.com/seangwright/xperience-community-preview-component-outlines/compare).

Please refer to the [Code of Conduct](./CODE_OF_CONDUCT.md) when contributing to or opening issues for this repository.
