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
        await _page.GotoAsync(baseUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle, Timeout = 30000 });
    }

    public async Task<bool> IsLoadedAsync()
    {
        // Wait for the page to be loaded by checking for common elements
        // Try multiple selectors since Server and WebAssembly might have different initial DOM
        try
        {
            // Wait for body to be present first
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 15000 });
            
            // Then check for either the app element or body content
            var selectors = new[] { "#app", "body", "main", "nav" };
            foreach (var selector in selectors)
            {
                var element = await _page.QuerySelectorAsync(selector);
                if (element != null)
                {
                    return true;
                }
            }
            
            return false;
        }
        catch
        {
            return false;
        }
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
