# Issue 0015: Integrate Playground into CI and documentation

- Status: open
- Role: developer
- Area: playground-ci
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: playgroundci
- External: none
- Pipeline: 14
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Update .github/workflows/build-and-test.yml to include Playground.UnitTests in the unit test step (it should already be picked up by the*.UnitTests.csproj glob, but verify). Update .github/workflows/publish-nuget.yml if needed to ensure the Playground nupkg is published. Update the root README.md to mention the newOptionA.Blazor.Playground package with a brief description and link to its readme. Ensure the Playground .csproj has PackageReleaseNotes set for the initial release. Verify the project builds with `dotnet build OptionA.Blazor.Playground/OptionA.Blazor.Playground.csproj --configuration Release` and tests pass with `dotnet test OptionA.Blazor.Playground.UnitTests/OptionA.Blazor.Playground.UnitTests.csproj`.

## Latest Run

(none)

## Recent Decisions

(none)