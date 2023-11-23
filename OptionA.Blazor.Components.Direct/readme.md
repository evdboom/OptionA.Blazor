# OptionA.Blazor.Components.Direct
Blazor components for use in your Blazor website, base set for the full components, but these components do not require setup to use.

For full documentation, releasenotes and examples, go to [option-a.tech](https://www.option-a.tech/documentation/blazor/components). To full source can be viewed on [github](https://github.com/evdboom/OptionA.Blazor).

## Getting started
To start using the OptionA.Blazor.Components.Direct, use the components provided by the package in your application. For styling all components have an "opta-[component-name]" attribute, or you can use the parameters on a percomponent basis (see below)

## Latest release notes
### 8.0.0
#### Overall
Update to .NET 8

#### New features
- Update package to .NET 8

#### Solved Bugs
- none

## Components
Following are the currently supported components. For all components there is a `Parameter` AdditionalClasses to provide specific classes for that single component. RemovedClasses to remove default classes (set by the config) for that single component, and an Attributes parameter to set additional required attributes to that single component.

#### Direct input
```
<OptAInputInteger>
<OptAInputText>
<OptAInputTextArea>
```
These three components are implementations of the the default microsoft `Microsoft.AspNetCore.Components.Forms` input classes. But binds the change event to `oninput` instead of `onchange` (when it looses focus). Also wraps them in the default OptAComponent for consistancy with other components.
The textarea components optionally supports AutoGrow to grow when the content changes (note: when using custom css, for instance bootstrap for the input, the mask used for the autogrow might need additional setup)
for instance, for the bootstrap form-control the following css is needed to make sure the autogrow maps as expected:

```
[auto-grow]::after {
    padding: .375rem .75rem;
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    width: 100%;
}
```

#### Enum select
```
<OptAEnumSelect>
```
Implementation of the microsoft `Microsoft.AspNetCore.Components.Forms.InputSelect`, but passed with a generic Enum, gives a select with all the enum values.
Also supports giving dictionaries for mapping displaynames or option titles to customize.