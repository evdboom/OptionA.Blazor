namespace OptionA.Blazor.E2E.PageObjects;

/// <summary>
/// Page object for the buttons test page.
/// </summary>
public class ButtonsPage
{
    private readonly IPage _page;
    private const string ButtonsPageUrl = "/components/buttons";
    private const string ShowButtonSelector = "button:has-text('Show')";
    private const string ButtonBarSelector = "[opta-button-bar]";
    private const string ButtonBarButtonSelector = $"{ButtonBarSelector} button";

    public ButtonsPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync(string baseUrl)
    {
        await _page.GotoAsync($"{baseUrl}{ButtonsPageUrl}", new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle, Timeout = 30000 });
    }

    public async Task<bool> IsLoadedAsync()
    {
        // Wait for page to load
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 15000 });

        // Check for the page's control buttons to be present
        return await _page.Locator(ShowButtonSelector).CountAsync() > 0;
    }

    public async Task<string> GetClickCountTextAsync()
    {
        // Get the text showing how many times buttons were clicked
        var paragraph = await _page.QuerySelectorAsync("p");
        if (paragraph == null)
        {
            return string.Empty;
        }
        return await paragraph.TextContentAsync() ?? string.Empty;
    }

    public async Task ClickShowButtonAsync()
    {
        // Click the "Show" button to display the button bar
        await _page.Locator(ShowButtonSelector).ClickAsync();
        await _page.Locator(ButtonBarSelector).WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000
        });
    }

    public async Task ClickFirstOptAButtonAsync()
    {
        // Click the first OptA button in the button bar
        var button = _page.Locator(ButtonBarButtonSelector).First;
        await button.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000
        });
        await button.ClickAsync();
    }

    public async Task<int> GetOptAButtonCountAsync()
    {
        // Count OptA buttons on the page
        return await _page.Locator(ButtonBarButtonSelector).CountAsync();
    }
}
