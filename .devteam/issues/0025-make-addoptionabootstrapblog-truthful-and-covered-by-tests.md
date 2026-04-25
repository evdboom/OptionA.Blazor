# Issue 0025: Make AddOptionABootstrapBlog truthful and covered by tests

- Status: open
- Role: fullstack-developer
- Area: blog-config
- Priority: 68
- Depends On: none
- Roadmap Item: 1
- Family: blog-config
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

What: either implement real bootstrap-prefill behavior for `AddOptionABootstrapBlog` or stop advertising it as distinct from `AddOptionABlog`. Why: `OptionA.Blazor.Blog\ServiceCollectionExtensions.cs` currently forwards caller configuration unchanged while the README says bootstrap defaults are prefilled, which is a silent contract mismatch. How: inspect existing blog option types, apply concrete bootstrap defaults where they already exist or rename/rewrite the docs/API contract to match actual behavior, and add tests that compare the bootstrap and non-bootstrap registration paths. AcceptanceCriteria: bootstrap registration has observable, asserted behavior distinct from the base helper or the docs/API text no longer claims that it does

## Latest Run

(none)

## Recent Decisions

(none)