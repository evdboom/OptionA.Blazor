# Issue 0022: Align DocumentComponentRegistry thread safety with PlaygroundRegistry

- Status: open
- Role: developer
- Area: blog-document
- Priority: 25
- Depends On: none
- Roadmap Item: 1
- Family: blogdocument
- External: none
- Pipeline: 16
- Pipeline Stage: 0
- Planning Issue: no

## Detail

`PlaygroundRegistry` (in Playground project) uses `ConcurrentDictionary` for thread-safe registration and lookup. Its sibling `DocumentComponentRegistry` (in Blog project, `Document/Internal/DocumentComponentRegistry.cs`) uses a plain `Dictionary<string, Type>`. Both are singleton registries. While writes happen only at startup today, the inconsistency is a maintenance trap — if a future consumer registers components dynamically (e.g., lazy module loading), the non-concurrent dictionary will produce race conditions. **Fix:** Replace the plain `Dictionary` in `DocumentComponentRegistry` with `ConcurrentDictionary`, or add a code comment documenting the startup-only-write contract. Prefer `ConcurrentDictionary` for consistency. **Acceptance criteria:** (1) `DocumentComponentRegistry._map` is `ConcurrentDictionary<string, Type>`

## Latest Run

(none)

## Recent Decisions

(none)