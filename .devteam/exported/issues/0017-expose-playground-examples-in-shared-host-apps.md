# Issue 0017: Expose playground examples in shared host apps

- Status: open
- Role: developer
- Area: playground-host
- Priority: 86
- Depends On: 0007, 0009, 0010, 0011, 0012, 0013
- Roadmap Item: 1
- Family: playgroundhost
- External: none
- Pipeline: 15
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Reuse the existing sample hosts instead of creating a new docs app. Update `OptionA.Blazor.Test\Program.cs`, `OptionA.Blazor.Server.Test\Program.cs`, `OptionA.Blazor.Test.Shared\OptionA.Blazor.Test.Shared.csproj`, `OptionA.Blazor.Test.Shared\Shared\MainLayout.razor`, and add `OptionA.Blazor.Test.Shared\Pages\Playground\ButtonsPlayground.razor(.cs)` plus `TabsPlayground.razor(.cs)`. Register `AddOptionABootstrapPlayground()`, add a single top-level Playground navigation group, and render descriptor-backed examples for at least Buttons and Tabs. Introduce `OptionA.Blazor.Test.Shared\Struct\Playground\IPlaygroundExampleCatalog.cs` and `PlaygroundExampleCatalog.cs` so example metadata is injected rather than hard-coded. Keep both `Program.cs` files bootstrap-only and keep each example page focused on one component family.

## Latest Run

(none)

## Recent Decisions

(none)