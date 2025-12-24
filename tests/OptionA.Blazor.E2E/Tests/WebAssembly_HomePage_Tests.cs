using OptionA.Blazor.E2E.Fixtures;
using OptionA.Blazor.E2E.PageObjects;

namespace OptionA.Blazor.E2E.Tests;

/// <summary>
/// E2E tests for WebAssembly mode - Home page scenarios.
/// </summary>
[Collection(nameof(WebAssemblyCollection))]
[Trait("Category", "E2E")]
public class WebAssembly_HomePage_Tests : PlaywrightTestBase
{
    private readonly WebAssemblyAppFixture _fixture;

    public WebAssembly_HomePage_Tests(WebAssemblyAppFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_WebAssemblyApp_When_NavigatingToHomePage_Then_PageLoadsSuccessfully()
    {
        // Arrange
        var page = GetPage();
        var homePage = new HomePage(page);

        // Act
        await homePage.NavigateAsync(_fixture.GetBaseUrl());

        // Assert
        var isLoaded = await homePage.IsLoadedAsync();
        Assert.True(isLoaded, "Home page should load successfully");
        
        var title = await homePage.GetTitleAsync();
        Assert.NotNull(title);
    }

    [Fact]
    public async Task Given_WebAssemblyApp_When_OnHomePage_Then_NavigationMenuIsPresent()
    {
        // Arrange
        var page = GetPage();
        var homePage = new HomePage(page);

        // Act
        await homePage.NavigateAsync(_fixture.GetBaseUrl());
        await homePage.IsLoadedAsync();

        // Assert
        var hasMenu = await homePage.HasNavigationMenuAsync();
        Assert.True(hasMenu, "Navigation menu should be present on the home page");
    }
}
