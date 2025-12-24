namespace OptionA.Blazor.E2E.PageObjects;

/// <summary>
/// Page object for the home/landing page.
/// </summary>
public class HomePage
{
    private readonly IPage _page;

    public HomePage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync(string baseUrl)
    {
        await _page.GotoAsync(baseUrl);
    }

    public async Task<bool> IsLoadedAsync()
    {
        // Wait for the page to be loaded by checking for the app element
        var appElement = await _page.WaitForSelectorAsync("#app", new PageWaitForSelectorOptions
        {
            State = WaitForSelectorState.Attached,
            Timeout = 10000
        });
        return appElement != null;
    }

    public async Task<string?> GetTitleAsync()
    {
        return await _page.TitleAsync();
    }

    public async Task<bool> HasNavigationMenuAsync()
    {
        // Check if navigation menu exists
        var menu = await _page.QuerySelectorAsync("nav");
        return menu != null;
    }
}
