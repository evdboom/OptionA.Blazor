# Issue 0006: Remove Blog.Builder references from solution and test project

- Status: open
- Role: frontend-developer
- Area: none
- Priority: 65
- Depends On: none
- Roadmap Item: 1
- Family: removeblogbuilderreferencesfromsolutionandtestproject
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Two references to the retired Blog.Builder remain: (1) `OptionA.Blazor.Blog.Builder` project in `OptionA.Blazor.sln` (GUID `{1CF6B926-0BF8-48D4-B76E-E5CE7AA2F87D}`), (2) `<ProjectReference Include="..\OptionA.Blazor.Blog.Builder\OptionA.Blazor.Blog.Builder.csproj" />` in `OptionA.Blazor.Blog.UnitTests\OptionA.Blazor.Blog.UnitTests.csproj`. Remove both. Do NOT delete the Blog.Builder folder or its NuGet package. Verify `dotnet build OptionA.Blazor.sln --configuration Release` still succeeds with 0 errors. If any test references Blog.Builder types, remove/replace those test files. FilesInScope: `OptionA.Blazor.sln`, `OptionA.Blazor.Blog.UnitTests\OptionA.Blazor.Blog.UnitTests.csproj`. Acceptance criteria: (1) `dotnet sln OptionA.Blazor.sln list` does not include Blog.Builder, (2) Blog.UnitTests.csproj has no Blog.Builder ProjectReference, (3) solution builds cleanly, (4) all unit tests still pass. Supersedes issue #4.

## Latest Run

(none)

## Recent Decisions

(none)