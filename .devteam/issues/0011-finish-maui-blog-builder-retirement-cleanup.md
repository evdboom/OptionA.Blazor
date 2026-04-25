# Issue 0011: Finish MAUI Blog.Builder retirement cleanup

- Status: open
- Role: frontend-developer
- Area: none
- Priority: 90
- Depends On: 0003
- Roadmap Item: 1
- Family: finishmauiblogbuilderretirementcleanup
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

OptionA.Blazor.Maui.Test no longer references OptionA.Blazor.Blog.Builder, but stale Blog/Builder usage remains in Components\_Imports.razor, Components\Layout\MainLayout.razor, Components\Pages\BlogBuilder.razor, and Components\Pages\BlogBuilder.razor.cs. Align the MAUI test app with the intended retired stub/removal behavior and make the MAUI project build cleanly without reintroducing the Builder package.

## Latest Run

(none)

## Recent Decisions

(none)