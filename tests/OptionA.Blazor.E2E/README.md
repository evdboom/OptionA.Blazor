# OptionA.Blazor.E2E

End-to-end (E2E) tests for OptionA.Blazor components using Playwright and xUnit.

## Overview

This project contains automated E2E tests that validate the behavior of OptionA.Blazor components in real browser environments. Tests are executed against both Blazor WebAssembly and Blazor Server hosting models to ensure compatibility across different rendering modes.

## Prerequisites

- .NET 10.0 SDK or later
- PowerShell (for running Playwright installation script)

## Getting Started

### Installing Playwright Browsers

Before running the tests for the first time, you need to install the Playwright browsers:

```powershell
# Build the E2E project first
dotnet build tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj --configuration Release

# Install Playwright browsers (Chromium is used for tests)
pwsh tests/OptionA.Blazor.E2E/bin/Release/net10.0/playwright.ps1 install --with-deps chromium
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

## Architecture

### Fixtures

The test infrastructure uses xUnit collection fixtures to manage application lifecycle:

- **BlazorAppFixture**: Base fixture that handles starting/stopping test applications
- **WebAssemblyAppFixture**: Manages the Blazor WebAssembly test app using `dotnet run`
- **ServerAppFixture**: Manages the Blazor Server test app using `dotnet run`

Both fixtures use dynamic port allocation (`http://127.0.0.1:0`) to avoid port conflicts and run applications without publishing for faster test execution.

### Page Objects

Page objects encapsulate UI interactions and provide a clean API for tests:

- **HomePage**: Interactions with the home/landing page
- **ButtonsPage**: Interactions with the buttons test page

Page objects handle waiting for elements, navigation, and querying the DOM.

## Troubleshooting

### Playwright Browsers Not Found

**Error:** `Executable doesn't exist at [path]`

**Solution:** Install Playwright browsers as described in the "Installing Playwright Browsers" section above.

### Tests Timing Out

**Issue:** Tests fail with timeout errors

**Solutions:**
- Increase timeout values in page object methods if necessary
- Check that the test applications (OptionA.Blazor.Test and OptionA.Blazor.Server.Test) build correctly
- Ensure no other processes are using required ports
- Run tests with `--verbosity detailed` to see detailed logs including app output

### Port Already in Use

**Issue:** Application fails to start because port is in use

**Solution:** The test fixtures use dynamic port allocation (`http://127.0.0.1:0`), which should automatically find an available port. If issues persist, check for zombie processes and terminate them.

### Browser Launch Failures

**Issue:** Browser fails to launch on CI or locally

**Solutions:**
- On CI: Ensure the workflow installs browsers with `--with-deps` flag
- On Linux: Install required system dependencies: `apt-get install libnss3 libatk-bridge2.0-0 libdrm2 libxkbcommon0 libgbm1 libasound2`
- On Windows: Ensure PowerShell execution policy allows script execution

### Test Applications Not Building

**Issue:** E2E tests fail because test applications don't build

**Solution:**
- Manually build the test applications first:
  ```powershell
  dotnet build OptionA.Blazor.Test --configuration Release
  dotnet build OptionA.Blazor.Server.Test --configuration Release
  ```
- Check for build errors and resolve them before running E2E tests

### Dynamic Port Binding Error

**Issue:** `Dynamic port binding is not supported when binding to localhost`

**Solution:** This is a .NET 10 requirement. The fixtures already use `http://127.0.0.1:0` instead of `http://localhost:0`. If you see this error, verify that your fixture uses the correct URL format.

## Adding New Tests

To add new E2E tests:

1. Create a new test class in the `Tests/` folder
2. Inherit from `PlaywrightTestBase`
3. Add the `[Trait("Category", "E2E")]` attribute
4. Use the `[Collection]` attribute to specify which app fixture to use (WebAssemblyCollection or ServerCollection)
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
3. The browser will launch in headless mode by default

To run tests in non-headless mode for easier inspection, modify `PlaywrightTestBase.cs` and set `Headless = false` in the `LaunchAsync` options.

## Performance Considerations

- E2E tests are slower than unit tests by nature
- Tests share application instances via xUnit collection fixtures to improve performance
- Each test gets its own browser context and page to ensure isolation
- Applications run using `dotnet run --no-build` to avoid repeated publishing overhead
- Dynamic port allocation prevents port conflicts when running tests in parallel

## Known Limitations

- Page titles may be empty in some Blazor Server scenarios - tests have been adjusted to not rely on specific titles
- Some OptionA components that depend on `IJSRuntime` may have limited functionality in Server mode due to service lifetime differences
- Tests use Chromium browser only (other browsers can be added if needed)

## Further Reading

- [Playwright for .NET Documentation](https://playwright.dev/dotnet/)
- [xUnit Documentation](https://xunit.net/)
- [Blazor Testing Best Practices](https://learn.microsoft.com/en-us/aspnet/core/blazor/test)
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
