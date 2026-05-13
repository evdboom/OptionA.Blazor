# Issue 0021: Remove unnecessary AllowUnsafeBlocks from Blog.csproj

- Status: open
- Role: frontend-developer
- Area: blog
- Priority: 30
- Depends On: none
- Roadmap Item: 1
- Family: blog
- External: none
- Pipeline: 15
- Pipeline Stage: 0
- Planning Issue: no

## Detail

`OptionA.Blazor.Blog/OptionA.Blazor.Blog.csproj` line 6 sets `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>`. No source file in the Blog project uses `unsafe` code — the Markdown pipeline is pure managed C#. This was likely cargo-culted from the Helpers or Blog.Builder projects. **Acceptance criteria:** (1) `AllowUnsafeBlocks` removed from Blog.csproj

## Latest Run

(none)

## Recent Decisions

(none)