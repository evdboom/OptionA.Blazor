# Issue 0095: Audit BlogBuilder and propose migration split

- Status: done
- Role: developer
- Area: migration
- Priority: 70
- Depends On: none
- Roadmap Item: 1
- Family: migration
- External: none
- Pipeline: 65
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Review OptionA.Blazor.Blog.Builder features. Create a short migration plan: list features to keep (content model, asset pipeline), features to rewrite as generic packages (editor, playground integration), and features to drop. Produce execution tasks to move necessary bits into Interactive or separate packages.

## Latest Run

- Run: 74
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-developer-13e67ba79008
- Updated: 2026-04-22T17:35:38.1382089+00:00
- Summary: Audited OptionA.Blazor.Blog.Builder (project files, components, service registration, parts, JS interop). Identified concrete features to keep (core models, asset pipeline), to move into new generic packages (editor/runtime UI, Monaco integration, live preview/playground integration), and features to drop (legacy WYSIWYG-specific scaffolding and tight blog-only service defaults). Produced a short migration plan and a set of execution tasks to extract, repackage, implement Interactive, and deprecate the old builder.
- Skills Used: (none)
- Tools Used: functions.report_intent- functions.view
- Changed Files: none

## Recent Decisions

- #164 [run] Run #74 Completed: Audited OptionA.Blazor.Blog.Builder (project files, components, service registration, parts, JS interop). Identified concrete features to keep (core models, asset pipeline), to move into new generic packages (editor/runtime UI, Monaco integration, live preview/playground integration), and features to drop (legacy WYSIWYG-specific scaffolding and tight blog-only service defaults). Produced a short migration plan and a set of execution tasks to extract, repackage, implement Interactive, and deprecate the old builder.