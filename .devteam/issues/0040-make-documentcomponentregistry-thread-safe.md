# Issue 0040: Make DocumentComponentRegistry thread-safe

- Status: open
- Role: frontend-developer
- Area: blog-document
- Priority: 35
- Depends On: none
- Roadmap Item: 1
- Family: blogdocument
- External: none
- Pipeline: 28
- Pipeline Stage: 0
- Planning Issue: no

## Detail

`DocumentComponentRegistry` uses `Dictionary<string, Type>` while `PlaygroundRegistry` correctly uses `ConcurrentDictionary`. Although registration typically happens at startup, `AddDocumentComponent<T>()` can technically be called post-build. Switch to `ConcurrentDictionary` for consistency and safety. Files in scope: `OptionA.Blazor.Blog/Document/Internal/DocumentComponentRegistry.cs`. Acceptance criteria: (1) Backing store is `ConcurrentDictionary`

## Latest Run

(none)

## Recent Decisions

(none)