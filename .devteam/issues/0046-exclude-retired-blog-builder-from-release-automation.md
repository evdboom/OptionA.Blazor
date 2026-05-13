# Issue 0046: Exclude retired Blog.Builder from release automation

- Status: done
- Role: devops
- Area: blog-builder-retirement
- Priority: 90
- Depends On: none
- Roadmap Item: 1
- Family: blogbuilderretirement
- External: none
- Pipeline: 32
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: stop `OptionA.Blazor.Blog.Builder` from being auto-selected for NuGet release. Why: the retirement decision says the package stays on NuGet at its current version and should not ship new versions, but `.github/workflows/publish-nuget.yml` still discovers every packable project (`Is-PackableProject` at lines70-85

## Latest Run

- Run: 28
- Status: Completed
- Model: gpt-5.4
- Session: devteam-devops-14a8a782601f
- Updated: 2026-05-13T14:23:10.0401187+00:00
- Summary: Updated `.github/workflows/publish-nuget.yml` so `OptionA.Blazor.Blog.Builder.csproj` is treated as a retired package and excluded from `Is-PackableProject` selection. That keeps the project buildable in the repo while preventing it from being auto-selected, packed, published, or attached to future NuGet releases. The local packable-project check no longer lists Blog.Builder, and `dotnet build .\OptionA.Blazor.sln --configuration Release` completed successfully.
- Skills Used: none
- Tools Used: view- rg- apply_patch- powershell- `git --no-pager status --short`, `git --no-pager diff -- .github/workflows/publish-nuget.yml`, `dotnet build .\OptionA.Blazor.sln --configuration Release`
- Changed Files: none

## Recent Decisions

(none)