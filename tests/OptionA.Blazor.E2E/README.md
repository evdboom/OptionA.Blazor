# OptionA.Blazor.E2E

End-to-end (E2E) tests for OptionA.Blazor components using Playwright and xUnit.

## Overview

This project contains automated E2E tests that validate the behavior of OptionA.Blazor components in real browser environments. Tests are executed against both Blazor WebAssembly and Blazor Server hosting models to ensure compatibility across different rendering modes.

## Prerequisites

- .NET 10.0 SDK or later
- Node.js (for Playwright browser installation)

## Getting Started

### Installing Playwright Browsers

Before running the tests for the first time, you need to install the Playwright browsers:

```powershell
# Build the E2E project first
dotnet build tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj

# Install Playwright browsers
pwsh tests/OptionA.Blazor.E2E/bin/Debug/net10.0/playwright.ps1 install
```

Alternatively, you can use the NPX method:

```bash
npx playwright install --with-deps chromium
```

### Running E2E Tests Locally

To run all E2E tests:

```powershell
dotnet test --filter "Category=E2E"
```

To run tests for a specific rendering mode:

```powershell
# WebAssembly tests only
dotnet test tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj --filter "FullyQualifiedName~WebAssembly"

# Server tests only
dotnet test tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj --filter "FullyQualifiedName~Server"
```

To run a specific test class:

```powershell
dotnet test tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj --filter "FullyQualifiedName~ButtonInteraction"
```

### Running with Verbose Output

For debugging purposes, use verbose output:

```powershell
dotnet test tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj --filter "Category=E2E" --verbosity detailed
```

## Test Structure

Tests are organized using the Given/When/Then naming convention:

```
Given_<Context>_When_<Action>_Then_<ExpectedOutcome>
```

For example:
- `Given_WebAssemblyApp_When_NavigatingToHomePage_Then_PageLoadsSuccessfully`
- `Given_ServerApp_When_ClickingOptAButton_Then_ClickCountIncreases`

### Test Organization

```
tests/OptionA.Blazor.E2E/
├── Fixtures/               # Test fixtures for app lifecycle management
│   ├── BlazorAppFixture.cs
│   ├── WebAssemblyAppFixture.cs
│   └── ServerAppFixture.cs
├── PageObjects/            # Page object models for UI interactions
│   ├── HomePage.cs
│   └── ButtonsPage.cs
├── Tests/                  # Actual test files
│   ├── WebAssembly_HomePage_Tests.cs
│   ├── Server_HomePage_Tests.cs
│   ├── WebAssembly_ButtonInteraction_Tests.cs
│   ├── Server_ButtonInteraction_Tests.cs
│   ├── WebAssembly_Navigation_Tests.cs
│   └── Server_Navigation_Tests.cs
└── PlaywrightTestBase.cs  # Base class for all tests
```

## Test Coverage

The E2E test suite covers the following scenarios:

### 1. Landing Page Rendering
- Verifies that the home page loads successfully
- Checks for navigation menu presence
- Tests for both WebAssembly and Server modes

### 2. Button Interaction
- Tests button display and interaction
- Validates state updates after button clicks
- Ensures click count increments correctly
- Tests for both WebAssembly and Server modes

### 3. Navigation Flow
- Tests navigation between pages
- Verifies correct page titles
- Ensures navigation state is preserved
- Tests for both WebAssembly and Server modes

## CI/CD Integration

E2E tests are automatically run in GitHub Actions on pull requests to the main branch. The workflow:

1. Builds the solution
2. Runs unit tests
3. Installs Playwright browsers
4. Runs E2E tests separately
5. Reports results independently from unit tests

See `.github/workflows/build-and-test.yml` for the full CI configuration.

## Troubleshooting

### Playwright Browsers Not Found

**Error:** `Executable doesn't exist at [path]`

**Solution:** Install Playwright browsers as described in the "Installing Playwright Browsers" section above.

### Tests Timing Out

**Issue:** Tests fail with timeout errors

**Solutions:**
- Increase timeout values in test code if necessary
- Check that the test applications are building correctly
- Ensure no other processes are using the required ports
- Run tests with `--verbosity detailed` to see detailed logs

### Port Already in Use

**Issue:** Application fails to start because port is in use

**Solution:** The test fixtures use dynamic port allocation (`--urls=http://localhost:0`), but if you encounter issues, check for processes holding ports and terminate them.

### Browser Launch Failures

**Issue:** Browser fails to launch on CI or locally

**Solutions:**
- On CI: Ensure the workflow installs browsers with `--with-deps` flag
- On Linux: Install required system dependencies: `apt-get install libnss3 libatk-bridge2.0-0 libdrm2 libxkbcommon0 libgbm1 libasound2`
- On Windows: Ensure PowerShell execution policy allows script execution

### Test Applications Not Building

**Issue:** E2E tests fail because test applications don't build

**Solution:**
- Manually build the test applications: `dotnet build OptionA.Blazor.Test` and `dotnet build OptionA.Blazor.Server.Test`
- Check for build errors and resolve them before running E2E tests

## Adding New Tests

To add new E2E tests:

1. Create a new test class in the `Tests/` folder
2. Inherit from `PlaywrightTestBase`
3. Add the `[Trait("Category", "E2E")]` attribute
4. Use the `[Collection]` attribute to specify which app fixture to use
5. Follow the Given/When/Then naming convention
6. Create page objects in `PageObjects/` for new pages as needed

Example:

```csharp
[Collection(nameof(WebAssemblyCollection))]
[Trait("Category", "E2E")]
public class WebAssembly_NewFeature_Tests : PlaywrightTestBase
{
    private readonly WebAssemblyAppFixture _fixture;

    public WebAssembly_NewFeature_Tests(WebAssemblyAppFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_Context_When_Action_Then_ExpectedOutcome()
    {
        // Arrange
        var page = GetPage();
        // ... test implementation
    }
}
```

## Debugging Tests

To debug tests in Visual Studio or VS Code:

1. Set a breakpoint in your test
2. Use "Debug Test" from the test explorer
3. The browser will launch in non-headless mode for easier inspection

To run tests in non-headless mode from CLI, modify `PlaywrightTestBase.cs` and set `Headless = false`.

## Performance Considerations

- E2E tests are slower than unit tests by nature
- Tests share application instances via xUnit collection fixtures to improve performance
- Each test gets its own browser context and page to ensure isolation
- Consider running E2E tests in parallel with `dotnet test --parallel`

## Further Reading

- [Playwright for .NET Documentation](https://playwright.dev/dotnet/)
- [xUnit Documentation](https://xunit.net/)
- [Blazor Testing Best Practices](https://learn.microsoft.com/en-us/aspnet/core/blazor/test)
