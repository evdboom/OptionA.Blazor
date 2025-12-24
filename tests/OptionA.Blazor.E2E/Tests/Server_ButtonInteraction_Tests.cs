using OptionA.Blazor.E2E.Fixtures;
using OptionA.Blazor.E2E.PageObjects;

namespace OptionA.Blazor.E2E.Tests;

/// <summary>
/// E2E tests for Server mode - Button interaction scenarios.
/// </summary>
[Collection(nameof(ServerCollection))]
[Trait("Category", "E2E")]
public class Server_ButtonInteraction_Tests : PlaywrightTestBase
{
    private readonly ServerAppFixture _fixture;

    public Server_ButtonInteraction_Tests(ServerAppFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_ServerApp_When_NavigatingToButtonsPage_Then_PageLoadsWithButtons()
    {
        // Arrange
        var page = GetPage();
        var buttonsPage = new ButtonsPage(page);

        // Act
        await buttonsPage.NavigateAsync(_fixture.GetBaseUrl());

        // Assert
        var isLoaded = await buttonsPage.IsLoadedAsync();
        Assert.True(isLoaded, "Buttons page should load successfully");
    }

    [Fact]
    public async Task Given_ServerApp_When_ClickingShowButton_Then_OptAButtonsAppear()
    {
        // Arrange
        var page = GetPage();
        var buttonsPage = new ButtonsPage(page);
        await buttonsPage.NavigateAsync(_fixture.GetBaseUrl());
        await buttonsPage.IsLoadedAsync();

        // Act
        await buttonsPage.ClickShowButtonAsync();

        // Assert
        var buttonCount = await buttonsPage.GetOptAButtonCountAsync();
        Assert.True(buttonCount > 0, "OptA buttons should appear after clicking Show button");
    }

    [Fact]
    public async Task Given_ServerApp_When_ClickingOptAButton_Then_ClickCountIncreases()
    {
        // Arrange
        var page = GetPage();
        var buttonsPage = new ButtonsPage(page);
        await buttonsPage.NavigateAsync(_fixture.GetBaseUrl());
        await buttonsPage.IsLoadedAsync();
        await buttonsPage.ClickShowButtonAsync();

        var initialText = await buttonsPage.GetClickCountTextAsync();

        // Act
        await buttonsPage.ClickFirstOptAButtonAsync();

        // Assert
        var updatedText = await buttonsPage.GetClickCountTextAsync();
        Assert.NotEqual(initialText, updatedText);
        Assert.Contains("1", updatedText); // Should show at least 1 click
    }
}
