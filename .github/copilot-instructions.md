# GitHub Copilot Instructions for OptionA.Blazor

## Repository Overview

This repository contains OptionA.Blazor - a collection of Blazor component libraries packaged as separate NuGet packages. The repository includes multiple projects for components, storage, blog functionality, and their corresponding unit tests.

Full documentation is available at [option-a.tech](https://www.option-a.tech).

## Technology Stack

- **.NET 10.0** - Primary framework
- **Blazor** - Web UI framework using Razor components
- **C#** - Primary programming language
- **Razor** - Component markup language
- **xUnit** - Testing framework
- **bUnit** - Blazor component testing library
- **Moq** - Mocking framework for unit tests

## Project Structure

The repository is organized into the following main projects:

- **OptionA.Blazor.Components** - Main component library (buttons, carousel, gallery, menu, modal, responsive, splitter, message box, tabs)
- **OptionA.Blazor.Components.Direct** - Direct components
- **OptionA.Blazor.Components.UnitTests** - Unit tests for components
- **OptionA.Blazor.Blog** - Blog-related components (table, quote, etc.)
- **OptionA.Blazor.Blog.Builder** - Blog builder functionality
- **OptionA.Blazor.Blog.UnitTests** - Unit tests for blog components
- **OptionA.Blazor.Storage** - Storage functionality
- **OptionA.Blazor.Storage.Contracts** - Storage contracts
- **OptionA.Blazor.Test** - Test utilities
- **OptionA.Blazor.Maui.Test** - MAUI-specific tests

Each project is packaged as a separate NuGet package with its own readme.md file containing documentation.

## Build and Test Commands

### Restore Dependencies
```powershell
Get-ChildItem -Recurse -Filter *.csproj | Where-Object { $_.Name -notmatch '\.Test\.csproj$' } | ForEach-Object {
    dotnet restore $_.FullName
}
```

### Build Projects
```powershell
Get-ChildItem -Recurse -Filter *.csproj | Where-Object { $_.Name -notmatch '\.Test\.csproj$' } | ForEach-Object {
    dotnet build $_.FullName --no-restore --configuration Release
}
```

### Run Unit Tests
```powershell
Get-ChildItem -Recurse -Filter *.csproj | Where-Object { $_.Name -match '\.UnitTests\.csproj$' } | ForEach-Object {
    dotnet test $_.FullName --verbosity normal --no-build --configuration Release
}
```

Or for a specific test project:
```bash
dotnet test OptionA.Blazor.Components.UnitTests/OptionA.Blazor.Components.UnitTests.csproj
```

## Coding Conventions

### General Guidelines

1. **Nullable Reference Types**: Enabled across all projects - use nullable annotations appropriately
2. **Implicit Usings**: Enabled - common namespaces are automatically imported
3. **Documentation**: XML documentation is generated for all projects - add XML doc comments to public APIs
4. **File-scoped Namespaces**: Use file-scoped namespaces (namespace OptionA.Blazor.Components;)

### Naming Conventions

- **Components**: Prefix with `OptA` (e.g., `OptAButton`, `OptAMenu`, `OptACarousel`)
- **Services**: Use interface/implementation pattern (e.g., `IResponsiveService`, `ResponsiveService`)
- **Parameters**: Use descriptive names with proper casing (e.g., `ButtonType`, `ClickAction`)
- **Private fields**: Use underscore prefix (e.g., `_buttonDataProvider`, `_initialized`)

### Component Structure

Blazor components follow this pattern:
- Razor file (`.razor`) for markup
- Code-behind file (`.razor.cs`) for component logic when complex
- Parameters use `[Parameter]` attribute
- Cascading parameters use `[CascadingParameter]` attribute with optional Name property
- Services are injected via constructor (for services) or `[Inject]` attribute (for components)

### Testing Conventions

- Test classes inherit from `BunitContext` for component tests
- Use `Moq` for mocking dependencies
- Test method names follow pattern: `MethodName_Scenario_ExpectedBehavior`
- Structure tests with Arrange/Act/Assert comments
- Global usings are defined in `GlobalUsings.cs` files

Example test pattern:
```csharp
[Fact]
public void ComponentMethod_WithSpecificInput_ReturnsExpectedResult()
{
    // Arrange
    _mockService.Setup(s => s.Method()).Returns(expectedValue);
    var cut = Render<Component>(parameters => parameters.Add(p => p.Property, value));
    
    // Act
    var result = cut.Find("selector");
    
    // Assert
    Assert.Equal(expected, actual);
}
```

### Component Configuration

- Components support configuration through extension methods (e.g., `AddOptionAMenu`, `AddOptionABootstrapMenu`)
- Bootstrap versions pre-fill with Bootstrap 5.3 classes
- Use `AddOptionAComponents` or `AddOptionABootstrapComponents` to add all components at once
- All components support `AdditionalClasses`, `RemovedClasses`, and `Attributes` parameters

### JavaScript Interop

- Use lazy-loaded JS modules: `_moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", path).AsTask())`
- Implement `IDisposable` for components with JS interop
- Use `[JSInvokable]` attribute for methods called from JavaScript
- Clean up JS resources in `Dispose()` method

### Version Numbers

- Version numbers align with .NET version (e.g., version 10.0.0 for .NET 10)
- Update version in `.csproj` files when making releases
- Include meaningful release notes in `PackageReleaseNotes` property

## Component Development Guidelines

When creating or modifying components:

1. **Configuration**: Create an options class for component configuration
2. **Data Providers**: Use data provider interfaces for extensibility
3. **Events**: Expose events for component state changes (e.g., `WindowSizeChanged`, `DimensionChanged`)
4. **Cascading Parameters**: Use cascading parameters for shared state in component hierarchies
5. **Bootstrap Support**: Provide both generic and Bootstrap-specific configurations
6. **Responsiveness**: Consider responsive design using the `OptAResponsive` component when applicable

## Package Management

- Each project produces a NuGet package with `GeneratePackageOnBuild` enabled
- Include `readme.md` in package via `PackageReadmeFile` property
- Set appropriate metadata: `Authors`, `PackageProjectUrl`, `PackageLicenseExpression`, `Description`
- Reference the main GitHub repository: https://github.com/evdboom/OptionA.Blazor

## CI/CD

The repository uses GitHub Actions for CI/CD:
- **build-and-test.yml**: Builds and tests on PRs to main branch
- **publish-nuget.yml**: Publishes packages to NuGet (details in workflow file)

All workflows use PowerShell as the default shell.

## Common Patterns

### Service Registration
```csharp
services.AddOptionAComponents(); // or AddOptionABootstrapComponents()
// Or individual components:
services.AddOptionAMenu();
services.AddOptionACarousel();
```

### Component Usage
```razor
<OptAComponent Parameter="value" AdditionalClasses="custom-class">
    <ChildContent>
        @* Component content *@
    </ChildContent>
</OptAComponent>
```

### Dependency Injection in Components
```csharp
[Inject]
private IServiceName ServiceName { get; set; } = default!;
```

### Dependency Injection in Services
```csharp
public ServiceName(IDependency dependency, IOtherDependency other)
{
    _dependency = dependency;
    _other = other;
}
```

## Documentation

- Each project folder contains a `readme.md` with component-specific documentation
- Include usage examples in readme files
- Document all public APIs with XML comments
- Keep release notes up to date in readme files and .csproj files
