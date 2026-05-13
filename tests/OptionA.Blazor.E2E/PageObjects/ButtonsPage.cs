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
    private const string ClickCountSelector = "p:has-text('Clicked')";

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
        // Click the "Show" button and verify the button bar appears. Retries handle the brief
        // window where Server's SignalR circuit or WebAssembly's runtime has not finished attaching
        // @onclick handlers to the prerendered DOM.
        await PlaywrightTestBase.ClickInteractiveAsync(
            _page.Locator(ShowButtonSelector),
            _page.Locator(ButtonBarSelector));
    }

    public async Task ClickFirstOptAButtonAsync()
    {
        // Click the first OptA button in the button bar, retrying to tolerate the brief window
        // where the freshly re-rendered button bar has not yet had its @onclick handler wired up
        // by the Server SignalR circuit or WebAssembly runtime.
        var button = _page.Locator(ButtonBarButtonSelector).First;
        await button.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000,
        });

        await PlaywrightTestBase.InteractAndVerifyAsync(
            () => button.ClickAsync(),
            _page.Locator($"{ClickCountSelector}:has-text('1')"));
    }

    public async Task<int> GetOptAButtonCountAsync()
    {
        // Count OptA buttons on the page
        return await _page.Locator(ButtonBarButtonSelector).CountAsync();
    }
}
