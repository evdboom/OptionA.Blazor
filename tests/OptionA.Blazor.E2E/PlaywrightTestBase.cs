namespace OptionA.Blazor.E2E;

/// <summary>
/// Base class for all Playwright E2E tests. Handles browser and page lifecycle.
/// </summary>
public abstract class PlaywrightTestBase : IAsyncLifetime
{
    protected IPlaywright? Playwright { get; private set; }
    protected IBrowser? Browser { get; private set; }
    protected IBrowserContext? Context { get; private set; }
    protected IPage? Page { get; private set; }

    public virtual async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1280, Height = 720 }
        });
        Page = await Context.NewPageAsync();
    }

    public virtual async Task DisposeAsync()
    {
        if (Page != null)
        {
            await Page.CloseAsync();
        }

        if (Context != null)
        {
            await Context.CloseAsync();
        }

        if (Browser != null)
        {
            await Browser.CloseAsync();
        }

        Playwright?.Dispose();
    }

    protected IPage GetPage()
    {
        if (Page == null)
        {
            throw new InvalidOperationException("Page not initialized. Ensure InitializeAsync has been called.");
        }
        return Page;
    }

    /// <summary>
    /// Performs an interaction (click/hover) and verifies the expected DOM mutation appears.
    /// Retries the interaction to tolerate the brief window where the page is prerendered but
    /// Blazor's Server SignalR circuit or WebAssembly runtime has not finished wiring up
    /// @onclick/@onmouseenter handlers.
    /// </summary>
    public static async Task InteractAndVerifyAsync(
        Func<Task> interaction,
        ILocator expectedAfterInteraction,
        int attempts = 6,
        int perAttemptTimeoutMs = 4000)
    {
        Exception? last = null;
        for (var i = 0; i < attempts; i++)
        {
            try
            {
                await interaction();
                await expectedAfterInteraction.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = perAttemptTimeoutMs,
                });
                return;
            }
            catch (TimeoutException ex) when (i < attempts - 1)
            {
                last = ex;
                // Circuit/runtime likely not attached yet; try again.
            }
        }
        if (last is not null)
        {
            throw last;
        }
    }

    public static Task ClickInteractiveAsync(ILocator clickTarget, ILocator expectedAfterClick) =>
        InteractAndVerifyAsync(() => clickTarget.ClickAsync(), expectedAfterClick);

    public static Task HoverInteractiveAsync(ILocator hoverTarget, ILocator expectedAfterHover) =>
        InteractAndVerifyAsync(() => hoverTarget.HoverAsync(), expectedAfterHover);
}
