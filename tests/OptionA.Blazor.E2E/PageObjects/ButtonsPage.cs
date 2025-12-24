namespace OptionA.Blazor.E2E.PageObjects;

/// <summary>
/// Page object for the buttons test page.
/// </summary>
public class ButtonsPage
{
    private readonly IPage _page;
    private const string ButtonsPageUrl = "/components/buttons";

    public ButtonsPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync(string baseUrl)
    {
        await _page.GotoAsync($"{baseUrl}{ButtonsPageUrl}");
    }

    public async Task<bool> IsLoadedAsync()
    {
        // Wait for the OptAButtonBar component to be present
        var buttonBar = await _page.WaitForSelectorAsync("button", new PageWaitForSelectorOptions
        {
            State = WaitForSelectorState.Attached,
            Timeout = 10000
        });
        return buttonBar != null;
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
        var showButton = await _page.QuerySelectorAsync("button:has-text('Show')");
        if (showButton != null)
        {
            await showButton.ClickAsync();
            // Wait for buttons to appear
            await _page.WaitForTimeoutAsync(500);
        }
    }

    public async Task ClickFirstOptAButtonAsync()
    {
        // Click the first OptA button in the button bar
        var optaButton = await _page.QuerySelectorAsync("[opta-button]");
        if (optaButton != null)
        {
            await optaButton.ClickAsync();
            // Wait for state to update
            await _page.WaitForTimeoutAsync(300);
        }
    }

    public async Task<int> GetOptAButtonCountAsync()
    {
        // Count OptA buttons on the page
        var buttons = await _page.QuerySelectorAllAsync("[opta-button]");
        return buttons.Count;
    }
}
