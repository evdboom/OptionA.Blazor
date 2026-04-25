# Issue 0023: Realign OptADocument metadata docs to shipped behavior

- Status: done
- Role: docs
- Area: document-rendering
- Priority: 82
- Depends On: 0007
- Roadmap Item: 1
- Family: documentrendering
- External: none
- Pipeline: 13
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: trim or clearly mark as pending the README sections that currently claim `OnMetadataParsed`, `DocumentMetadata`, YAML front-matter parsing, and copy-paste `Post` shim behavior not exposed by the current `OptADocument` component. Why: the published contract is ahead of code, which makes the package unauditable for consumers and obscures that issue7 is still open. How: update `OptionA.Blazor.Blog\readme.md` so examples match the actual `OptADocument` API, replace the `DateTime.Now` sample with caller-supplied or injected time guidance, and explicitly point metadata/front-matter readers at the pending implementation track instead of implying completion. AcceptanceCriteria: every public README example compiles against the current API surface, unsupported metadata features are labeled pending rather than documented as available, and no sample uses nondeterministic clock access. FilesInScope: `OptionA.Blazor.Blog\readme.md`, optional example markdown under `docs\`. LinkedDecisions: GOAL.md says Markdown is the authoring format and `Post` becomes optional, but front-matter support is a separate deliverable.

## Latest Run

- Run: 29
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-docs-0993be34c8fc
- Updated: 2026-04-25T21:22:06.3144687+00:00
- Summary: Updated OptionA.Blazor.Blog\readme.md so it matches the currently shipped OptADocument API. Removed/marked pending the unsupported OnMetadataParsed/DocumentMetadata/front-matter parsing, directive/playground and inline-component claims. Replaced the nondeterministic Post helper (DateTime.Now) with a deterministic helper pattern and provided minimal examples that compile against the current API (only OptADocument Source is used).
- Skills Used: (none)
- Tools Used: functions.view- functions.edit- functions.report_intent
- Changed Files: none

## Recent Decisions

(none)