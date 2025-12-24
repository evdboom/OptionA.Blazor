namespace OptionA.Blazor.E2E.Fixtures;

/// <summary>
/// Fixture for running E2E tests against the Blazor Server test application.
/// </summary>
public class ServerAppFixture : BlazorAppFixture
{
    protected override string ProjectPath => 
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "OptionA.Blazor.Server.Test", "OptionA.Blazor.Server.Test.csproj"));

    protected override string[] LaunchArguments =>
        new[] { "OptionA.Blazor.Server.Test.dll", "--urls=http://localhost:0" };
}

/// <summary>
/// Collection fixture for Server tests to share the same app instance.
/// </summary>
[CollectionDefinition(nameof(ServerCollection))]
public class ServerCollection : ICollectionFixture<ServerAppFixture>
{
}
