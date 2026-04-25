# Issue 0003: Retire OptionA.Blazor.Blog.Builder

- Status: inprogress
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
- Status: Running
- Model: claude-sonnet-4.6
- Session: devteam-frontend-developer-fe4f18b54e0e
- Updated: 2026-04-25T20:14:30.8991550+00:00
- Summary: 
- Skills Used: none
- Tools Used: none
- Changed Files: none

## Recent Decisions

(none)