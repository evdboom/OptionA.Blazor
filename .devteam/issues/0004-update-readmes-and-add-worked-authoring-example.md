# Issue 0004: Update readmes and add worked authoring example

- Status: done
- Role: docs
- Area: none
- Priority: 30
- Depends On: 0002
- Roadmap Item: 1
- Family: updatereadmesandaddworkedauthoringexample
- External: none
- Pipeline: 3
- Pipeline Stage: 0
- Planning Issue: no

## Detail

UpdateOptionA.Blazor.Blog/readme.md with OptADocument usage: authoring format (YAML front-matter, paragraph/heading/code/list/quote/table/image mapping, directive fences, inline tags), DocumentMetadata callback, and Post helper shim. Update OptionA.Blazor.Playground/readme.md with descriptor-registry flow: AddPlayground("id", descriptor), DescriptorId parameter, and error-block behaviour for unknown ids. Add a worked Markdown example file (e.g. docs/example-page.md) showing: one heading, one prose block with inline formatting, one ::: playground ::: directive, and one literal<OptA*> inline tag. FilesInScope: OptionA.Blazor.Blog/readme.md, OptionA.Blazor.Playground/readme.md, docs/example-page.md. Acceptance criteria: (1) each readme contains runnable copy-paste snippets for the new features

## Latest Run

- Run: 8
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-docs-bb822b2b7c69
- Updated: 2026-04-25T20:17:42.4114501+00:00
- Summary: Updated OptionA.Blazor.Blog/readme.md and OptionA.Blazor.Playground/readme.md with runnable, copy-paste examples (OptADocument directive & inline-tag usage, DocumentMetadata callback, Post helper shim, AddPlayground registry usage, DescriptorId behavior and authoring-error example). Created docs/example-page.md containing a heading, inline-formatted paragraph, a ::: playground ::: directive, and a literal <OptAButton ... /> inline tag. Built the Blog and Playground projects to verify changes (both succeeded).
- Skills Used: (none)
- Tools Used: report_intent- view- edit- create- glob- powershell
- Changed Files: none

## Recent Decisions

(none)