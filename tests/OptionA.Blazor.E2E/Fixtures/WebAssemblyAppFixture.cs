namespace OptionA.Blazor.E2E.Fixtures;

/// <summary>
/// Fixture for running E2E tests against the Blazor WebAssembly test application.
/// WebAssembly apps require a static file server to host the published content.
/// </summary>
public class WebAssemblyAppFixture : BlazorAppFixture
{
    protected override string ProjectPath => 
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "OptionA.Blazor.Test", "OptionA.Blazor.Test.csproj"));

    protected override string[] LaunchArguments =>
        new[] { "run", "--project", ProjectPath, "--no-build", "--configuration", "Release", "--urls=http://127.0.0.1:0" };
    
    protected override bool UsePublish => false;
}

/// <summary>
/// Collection fixture for WebAssembly tests to share the same app instance.
/// </summary>
[CollectionDefinition(nameof(WebAssemblyCollection))]
public class WebAssemblyCollection : ICollectionFixture<WebAssemblyAppFixture>
{
}
