# OptionA.Blazor
Blazor projects for various purposes, each in it's own nuget package.
visit [option-a.tech](https://www.option-a.tech) for full documentation, releasenotes and examples, or for a generic overview, look at the readme files in each project

## Projects
- [Blog](/OptionA.Blazor.Blog/readme.md)
- [Blog builder](/OptionA.Blazor.Blog.Builder/readme.md)
- [Components](/OptionA.Blazor.Components/readme.md)
- [Direct components](/OptionA.Blazor.Components.Direct/readme.md)
- [Playground](/OptionA.Blazor.Playground/readme.md)
- [Storage](/OptionA.Blazor.Storage/readme.md)
- [Storage contracts](/OptionA.Blazor.Storage.Contracts/readme.md)

## Testing

### Unit Tests

Run unit tests for all projects:

```powershell
Get-ChildItem -Recurse -Filter *.csproj | Where-Object { $_.Name -match '\.UnitTests\.csproj$' } | ForEach-Object {
    dotnet test $_.FullName --verbosity normal
}
```

### End-to-End Tests

The repository includes comprehensive E2E tests using Playwright that validate component behavior in real browser environments for both Blazor WebAssembly and Blazor Server modes.

#### First-time Setup

Install Playwright browsers before running E2E tests:

```powershell
# Build the E2E project
dotnet build tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj

# Install Playwright browsers
pwsh tests/OptionA.Blazor.E2E/bin/Debug/net10.0/playwright.ps1 install
```

#### Running E2E Tests

```powershell
# Run all E2E tests
dotnet test --filter "Category=E2E"

# Run E2E tests for specific mode
dotnet test tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj --filter "FullyQualifiedName~WebAssembly"
dotnet test tests/OptionA.Blazor.E2E/OptionA.Blazor.E2E.csproj --filter "FullyQualifiedName~Server"
```

For more details, see the [E2E Testing README](/tests/OptionA.Blazor.E2E/README.md).

DevTeam runtime authoritative policy: see .devteam/PERSISTENCE.md — .devteam/state/issues.json is the single source-of-truth for issue and workspace state. Maintain the validation checklist in .devteam/README.md and do not delete or untrack the .devteam directory; CI (issue #69) will validate its presence.

## DevTeam CI: .devteam integrity checks

A GitHub Actions workflow (.github/workflows/devteam-integrity.yml) verifies the presence of critical .devteam runtime artifacts (for example: .devteam/state/issues.json, .devteam/runs/, .devteam/plan.md). The workflow runs the reconciliation script in dry-run mode, executes the Pester coverage for .devteam/scripts/reconcile-workspace-state.ps1, and uploads an integrity artifact bundle containing the dry-run log, Pester transcript, NUnit XML results, and a timestamped .devteam backup zip for inspection. The workflow will fail when critical files are missing or the reconcile validation tests fail.

See .devteam/scripts/reconcile-workspace-state.ps1 for the script implementation and configured behavior.
