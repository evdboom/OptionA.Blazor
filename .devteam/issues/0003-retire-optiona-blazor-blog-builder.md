# Issue 0003: Retire OptionA.Blazor.Blog.Builder

- Status: done
- Role: frontend-developer
- Area: none
- Priority: 40
- Depends On: 0002
- Roadmap Item: 1
- Family: retireoptionablazorblogbuilder
- External: none
- Pipeline: 2
- Pipeline Stage: 0
- Planning Issue: no

## Detail

UpdateOptionA.Blazor.Blog.Builder/readme.md to document retirement: explain that the WYSIWYG block builder is no longer the recommended authoring path and direct authors to OptADocument + the Markdown format instead. Remove OptionA.Blazor.Blog.Builder from any solution include, playground reference, or test project reference that is not strictly required for existing passing tests. Do NOT ship new NuGet versions. FilesInScope: OptionA.Blazor.Blog.Builder/readme.md, OptionA.Blazor.sln, any .csproj that references the Builder package. Acceptance criteria: (1) readme updated with clear retirement notice and pointer to OptADocument

## Latest Run

- Run: 7
- Status: Completed
- Model: claude-sonnet-4.6
- Session: devteam-frontend-developer-fe4f18b54e0e
- Updated: 2026-04-25T20:19:54.6846498+00:00
- Summary: Retired `OptionA.Blazor.Blog.Builder` across the solution:

1. **readme.md** — Replaced the weak deprecation notice with a proper retirement notice (⚠️ header, explanation that WYSIWYG is not the recommended path) and a migration guide showing the Markdown authoring pattern with `OptADocument`.

2. **Removed Blog.Builder ProjectReferences** from all playground/demo apps: `OptionA.Blazor.Test`, `OptionA.Blazor.Server.Test`, `OptionA.Blazor.Maui.Test`, `OptionA.Blazor.Test.Shared`. Added direct `OptionA.Blazor.Blog` references to Test and Server.Test (they were getting Blog transitively via Builder before).

3. **Cleaned up Program.cs** in all three test/demo apps — removed `AddOptionABootstrapBlogBuilder` calls and related using statements.

4. **Replaced BlogBuilder.razor** with a static retirement notice stub; removed its code-behind entirely; removed the "BlogBuilder" nav link from `MainLayout.razor`.

5. **Blog.Builder stays in the solution** — `Blog.UnitTests` still tests the deprecated components (38 Builder-specific tests pass).

The one failing test (`OptADocument_DocumentationExample_UsesOnlyShippedSyntaxAndRendersSupportedNodes`) is pre-existing and unrelated to this issue — it tests that `docs/examples/buttons.md` doesn't use playground-directive syntax, but that file already contained `::: playground` before this change.
- Skills Used: verify
- Tools Used: view, edit, glob, grep, powershell
- Changed Files: none

## Recent Decisions

(none)