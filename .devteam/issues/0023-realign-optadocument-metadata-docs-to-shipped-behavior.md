# Issue 0023: Realign OptADocument metadata docs to shipped behavior

- Status: open
- Role: docs
- Area: document-rendering
- Priority: 82
- Depends On: 0007
- Roadmap Item: 1
- Family: document-rendering
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

What: trim or clearly mark as pending the README sections that currently claim `OnMetadataParsed`, `DocumentMetadata`, YAML front-matter parsing, and copy-paste `Post` shim behavior not exposed by the current `OptADocument` component. Why: the published contract is ahead of code, which makes the package unauditable for consumers and obscures that issue7 is still open. How: update `OptionA.Blazor.Blog\readme.md` so examples match the actual `OptADocument` API, replace the `DateTime.Now` sample with caller-supplied or injected time guidance, and explicitly point metadata/front-matter readers at the pending implementation track instead of implying completion. AcceptanceCriteria: every public README example compiles against the current API surface, unsupported metadata features are labeled pending rather than documented as available, and no sample uses nondeterministic clock access. FilesInScope: `OptionA.Blazor.Blog\readme.md`, optional example markdown under `docs\`. LinkedDecisions: GOAL.md says Markdown is the authoring format and `Post` becomes optional, but front-matter support is a separate deliverable.

## Latest Run

(none)

## Recent Decisions

(none)