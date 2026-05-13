# Issue 0047: Align contributor docs with Blog.Builder retirement

- Status: open
- Role: docs
- Area: blog-builder-retirement
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: blogbuilderretirement
- External: none
- Pipeline: 33
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: update contributor-facing documentation to present Blog.Builder as retired rather than active. Why: `README.md` lines5-11 still list “Blog builder” as a normal project, and `.github/copilot-instructions.md` lines21-34 still describe `OptionA.Blazor.Blog.Builder` as current project structure. That documentation now contradicts the repository direction and can steer future work back toward the retired path. How: mark the package as retired where it is listed, point authors to `OptionA.Blazor.Blog`/`OptADocument` and `OptionA.Blazor.Playground`, and make sure any high-level setup/build guidance no longer implies Builder is part of the active authoring story. Acceptance criteria: (1) root/contributor docs explicitly mark Blog.Builder retired, (2) docs point to Markdown + `OptADocument` as the recommended authoring flow, (3) no contributor-facing project list describes Builder as active without retirement context. FilesInScope: `README.md`, `.github/copilot-instructions.md`. Linked decisions: `OptionA.Blazor.Blog.Builder` is retired

## Latest Run

(none)

## Recent Decisions

(none)