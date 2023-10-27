# OptionA.Blazor.Components
Blazor components for use in your Blazor website

For full documentation, releasenotes and examples, go to [option-a.tech](https://www.option-a.tech/documentation/blazor/components). To full source can be viewed on [github](https://github.com/evdboom/OptionA.Blazor).

## Getting started
To start using the OptionA.Blazor.Components include the required depencenies in your service provider. The package uses the default .Net Dependency Injection.

### Service collection
To add the components you can use the extension method of each component, for example `AddOptionAMenu` or `AddOptionABootstrapMenu`. The bootstrap version prefills the optional configuration with bootstrap (5.3) classes. Everything in the config can be overwritten (if a config is supplied to the method this is applied after the default classes).
Alternatively to add all the components in the package use the `AddOptionAComponents` or `AddOptionABootstrapComponents` extension methods.

## Latest release notes
### 7.3.0 Naming
#### Overall
Naming in the package was not consistant, changed everything to OptA (instead of Opta).
Updated the used .Net packages to be consisted for all OptionA.Blazor packages.

#### New features
- Update the used .Net packages for consistency
#### Solved Bugs
- Changed all the naming to be consisted throughout the package **Breaking Change**
  - This requires updating the use of components to match the name, change the references from Opta... to OptA... 

## Components
Following are the currently supported components. For all components there is a `Parameter` AdditionalClasses to provide specific classes for that single component. RemovedClasses to remove default classes (set by the config) for that single component, and an Attributes parameter to set additional required attributes to that single component.

### Buttons
```
<OptAButton>
<OptAButtonBar>
```
Buttons for in you application. Supports a name and or an icon as content, the button bar can be used to create a horizontal or vertical bar with buttons at three position, start, center, end (depending on orientation).

### Carousel
```
<OptACarousel>
<OptACarouselSlide>
```
A Carousel for displaying images and content in a slideshow manner. The time between slides and whether or not autoplay is on can be set.

### Gallery
```
<OptAGallery>
<OptAGalleryImage>
```
A gallery for displaying thumbnails of images and enlarging them when selected, either in a side panel or in a modal

### Menu
```
<OptAmenu>
<OptAMenuDivider>
<OptAMenuGroup>
<OptAMenuItem>
```
A menu for creating site navigation, both horizontal and vertical. the group can hold multiple items and opend either on click or on mouseover.

### Modal
```
<OptAModal>
```
A modal using the html5 `<dialog>` tag. Optionally draggable to be able to move it around.

### Responsive
```
<OptAResponsive>
IResponsiveService
```
A component with no markup of it's own, but provides cascading parameters for the set breakpoints in the options. Optionally you can also inject the `IResponsiveService` in you components to provide the same information.

Usage:
```
<OptAResponsive>
	<SomeComponent /> @*Or other content*@
</OptAResponsive>
```

in SomeComponent:
```
<div>
    <div>@DimensionName</div>
    <div>@Dimension?.Name</div>
    <div>@Dimension?.Width</div>
    <div>@Dimension?.Height</div>
    @if (ValidDimensions != null)
    {
        @foreach(var dimension in ValidDimensions)
        {
            <div>@dimension</div>
        }
    }
    @if (BreakPoints != null)
    {
        @foreach(var breakPoint in BreakPoints)
        {
            <div>@breakPoint.Name @breakPoint.Width</div>
        }
    }
</div>

@code
{
    [CascadingParameter(Name = OptAResponsive.DimensionParameterName)]
    public NamedDimension? Dimension { get; set; }
    [CascadingParameter(Name = OptAResponsive.DimensionNameParameterName)]
    public string? DimensionName { get; set; }
    [CascadingParameter(Name = OptAResponsive.ValidDimensionsParameterName)]
    public IEnumerable<string>? ValidDimensions { get; set; }
    [CascadingParameter(Name = OptAResponsive.AllDimensionBreakPointsParameterName)]
    public IEnumerable<(string Name, int Width)>? BreakPoints { get; set; }
}
```

### Splitter
```
<OptASplitter>
```
A splitter component, creating a draggable bar in the center of two elements to change the relative size of the two.

Usage:
```
<OptASplitter Orientation="Orientation.Horizontal">
    <First>
        @*first part here*@
    </First>
    <Second>
        @*second part here*@
    </Second>
</OptASplitter>
```
