# Issue 0012: Finish retiring Blog.Builder from MAUI test app

- Status: open
- Role: frontend-developer
- Area: none
- Priority: 75
- Depends On: 0003
- Roadmap Item: 1
- Family: finishretiringblogbuilderfrommauitestapp
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

OptionA.Blazor.Maui.Test still contains the BlogBuilder page, code-behind, and OptionA.Blazor.Blog / OptionA.Blazor.Blog.Builder imports while its csproj now only references OptionA.Blazor.Components. Solution build currently fails with CS0234 and CS0246 from these stale MAUI test artifacts. Remove or replace the retired Blog.Builder surface without reintroducing the package, and restore a passing solution build.

## Latest Run

(none)

## Recent Decisions

(none)