namespace OptionA.Blazor.E2E.Fixtures;

/// <summary>
/// Fixture for running E2E tests against the Blazor WebAssembly test application.
/// </summary>
public class WebAssemblyAppFixture : BlazorAppFixture
{
    protected override string ProjectPath => 
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "OptionA.Blazor.Test", "OptionA.Blazor.Test.csproj"));

    protected override string[] LaunchArguments =>
        new[] { "OptionA.Blazor.Test.dll", "--urls=http://localhost:0" };
}

/// <summary>
/// Collection fixture for WebAssembly tests to share the same app instance.
/// </summary>
[CollectionDefinition(nameof(WebAssemblyCollection))]
public class WebAssemblyCollection : ICollectionFixture<WebAssemblyAppFixture>
{
}
