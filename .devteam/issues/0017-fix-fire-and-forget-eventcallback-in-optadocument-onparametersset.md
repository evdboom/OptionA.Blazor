# Issue 0017: Fix fire-and-forget EventCallback in OptADocument.OnParametersSet

- Status: done
- Role: frontend-developer
- Area: blog-document
- Priority: 72
- Depends On: none
- Roadmap Item: 1
- Family: blogdocument
- External: none
- Pipeline: 11
- Pipeline Stage: 0
- Planning Issue: no

## Detail

In `OptionA.Blazor.Blog/Document/OptADocument.razor.cs` line 39, `_ = OnMetadataParsed.InvokeAsync(metadata)` discards the task inside a synchronous lifecycle method. Exceptions from async callbacks are silently lost. **Fix:** Override `OnParametersSetAsync` instead of `OnParametersSet`, and `await` the `InvokeAsync` call. **Acceptance criteria:** (1) `OnParametersSetAsync` used

## Latest Run

- Run: 15
- Status: Completed
- Model: claude-sonnet-4.6
- Session: devteam-frontend-developer-8d5a2627e7e4
- Updated: 2026-05-13T13:52:17.9824454+00:00
- Summary: Replaced the synchronous `OnParametersSet` override with `OnParametersSetAsync` in `OptADocument.razor.cs` and awaited the `OnMetadataParsed.InvokeAsync(metadata)` call. This ensures exceptions from async callbacks propagate correctly instead of being silently discarded. All 186 blog unit tests pass.
- Skills Used: (none)
- Tools Used: view, powershell (file rewrite, build, test, git commit), grep, glob
- Changed Files: none

## Recent Decisions

(none)