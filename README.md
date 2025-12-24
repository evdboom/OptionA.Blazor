# OptionA.Blazor
Blazor projects for various purposes, each in it's own nuget package.
visit [option-a.tech](https://www.option-a.tech) for full documentation, releasenotes and examples, or for a generic overview, look at the readme files in each project

## Projects
- [Blog](/OptionA.Blazor.Blog/readme.md)
- [Blog builder](/OptionA.Blazor.Blog.Builder/readme.md)
- [Components](/OptionA.Blazor.Components/readme.md)
- [Direct components](/OptionA.Blazor.Components.Direct/readme.md)
- [Storage](/OptionA.Blazor.Storage/readme.md)
- [Storage contracts](/OptionA.Blazor.Storage.Contracts/readme.md)

## Testing

### Unit Tests

Run unit tests for all projects:

```powershell
Get-ChildItem -Recurse -Filter *.csproj | Where-Object { $_.Name -match '\.UnitTests\.csproj$' } | ForEach-Object {
    dotnet test $_.FullName --verbosity normal
}
```

### End-to-End Tests

The repository includes comprehensive E2E tests using Playwright that validate component behavior in real browser environments for both Blazor WebAssembly and Blazor Server modes.

#### First-time Setup

Install Playwright browsers before running E2E tests:

```powershell
# Build the E2E project
dotnet build tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj

# Install Playwright browsers
pwsh tests/OptionA.Blazor.E2E/bin/Debug/net10.0/playwright.ps1 install
```

#### Running E2E Tests

```powershell
# Run all E2E tests
dotnet test --filter "Category=E2E"

# Run E2E tests for specific mode
dotnet test tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj --filter "FullyQualifiedName~WebAssembly"
dotnet test tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj --filter "FullyQualifiedName~Server"
```

For more details, see the [E2E Testing README](/tests/OptionA.Blazor.E2E/README.md).
