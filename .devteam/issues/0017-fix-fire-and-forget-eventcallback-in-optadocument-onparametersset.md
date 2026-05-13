# Issue 0017: Fix fire-and-forget EventCallback in OptADocument.OnParametersSet

- Status: open
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

(none)

## Recent Decisions

(none)