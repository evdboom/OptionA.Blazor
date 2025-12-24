using OptionA.Blazor.E2E.Fixtures;
using OptionA.Blazor.E2E.PageObjects;

namespace OptionA.Blazor.E2E.Tests;

/// <summary>
/// E2E tests for Server mode - Navigation flow scenarios.
/// </summary>
[Collection(nameof(ServerCollection))]
[Trait("Category", "E2E")]
public class Server_Navigation_Tests : PlaywrightTestBase
{
    private readonly ServerAppFixture _fixture;

    public Server_Navigation_Tests(ServerAppFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_ServerApp_When_NavigatingFromHomeToButtons_Then_NavigationSucceeds()
    {
        // Arrange
        var page = GetPage();
        var homePage = new HomePage(page);
        await homePage.NavigateAsync(_fixture.GetBaseUrl());
        await homePage.IsLoadedAsync();

        // Act - Navigate to buttons page via URL
        var buttonsPage = new ButtonsPage(page);
        await buttonsPage.NavigateAsync(_fixture.GetBaseUrl());

        // Assert
        var isLoaded = await buttonsPage.IsLoadedAsync();
        Assert.True(isLoaded, "Should successfully navigate to buttons page");
        
        var title = await page.TitleAsync();
        Assert.Contains("Buttons", title);
    }

    [Fact]
    public async Task Given_ServerApp_When_NavigatingBackToHome_Then_ReturnsToHomePage()
    {
        // Arrange
        var page = GetPage();
        var buttonsPage = new ButtonsPage(page);
        await buttonsPage.NavigateAsync(_fixture.GetBaseUrl());
        await buttonsPage.IsLoadedAsync();

        // Act - Navigate back to home
        var homePage = new HomePage(page);
        await homePage.NavigateAsync(_fixture.GetBaseUrl());

        // Assert
        var isLoaded = await homePage.IsLoadedAsync();
        Assert.True(isLoaded, "Should successfully navigate back to home page");
        
        var title = await page.TitleAsync();
        Assert.Contains("Index", title);
    }
}
