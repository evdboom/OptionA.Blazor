# Issue 0002: Design the technical approach and create execution issues

- Status: done
- Role: architect
- Area: none
- Priority: 90
- Depends On: 0001
- Roadmap Item: 1
- Family: designthetechnicalapproachandcreateexecutionissues
- External: none
- Pipeline: 1
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Given the approved high-level plan, choose the technology stack, define the architecture, and break the work into concrete execution issues with clear dependencies.

## Latest Run

- Run: 4
- Status: Completed
- Model: gpt-5.4
- Session: devteam-architect-370bb67a7c3e
- Updated: 2026-04-25T20:08:36.8472562+00:00
- Summary: Current state is now clear: the repository already contains **GOAL.md items1 and2** in code (`OptionA.Blazor.Playground` descriptor registry and `OptionA.Blazor.Blog` base `OptADocument` Markdown renderer), while `.devteam` has been locally restored but is **untracked** in git. There is also runtime drift: open issues **#3** and **#4** already exist, and `OptionA.Blazor.Blog\readme.md` documents `OnMetadataParsed`, playground directives, and inline component tags before those features exist in code.

REFINEMENT:
- what: define the extension seam for items3–5 without changing the public package shape unnecessarily.
- why: the current `IMarkdownDocumentParser.Parse(string?) -> IReadOnlyList<IContent>` contract is too narrow for front-matter and directives, and broad `ContentType` changes would ripple into `OptAChild` and `BuilderService`.
- how: keep **Markdig internal** in `OptionA.Blazor.Blog`; add an **internal structured document parse result** that carries metadata plus ordered render items; parse `::: playground ... :::` and literal `<OptA*>` tags through dedicated document-processing stages instead of overloading `ParagraphContent`; keep rendering on the existing Blog components, use the existing `IPlaygroundDescriptorResolver`, and add a whitelist registry in Blog for inline components. Surface metadata through an `OnMetadataParsed` callback and a `Post` mapping helper, not through direct file or clock access.

FILES_IN_SCOPE:
- `OptionA.Blazor.Blog\Document\*`
- `OptionA.Blazor.Blog\Core\OptAChild.razor`
- `OptionA.Blazor.Blog\ServiceCollectionExtensions.cs`
- `OptionA.Blazor.Blog\Core\Post.cs`
- `OptionA.Blazor.Blog\Core\OptAPost.razor.cs`
- `OptionA.Blazor.Blog.UnitTests\Document\*`

LINKED_DECISIONS:
- #2: Approved high-level plan — entering architect planning- GOAL.md guiding decisions: Markdown is the authoring format; reuse existing Blog render components; keep Playground as the interactive surface; retire `OptionA.Blazor.Blog.Builder`; do not introduce a new package unless justifiedACCEPTANCE_CRITERIA:
- [ ] New work for items3–5 extends the existing Blog/Playground packages instead of creating a new package- [ ] Directive parsing, inline component rendering, and front-matter parsing are implemented through explicit internal seams rather than ad hoc paragraph hacks- [ ] Existing Markdown block rendering behavior remains intact- [ ] Non-trivial collaborators use constructor injection; no direct file system or clock dependency is introduced into the parser pipelineRISKS:
- Adding new `ContentType` values affects both `OptAChild` rendering and `BuilderService` serialization/deserialization- Documentation currently overstates implemented behavior and must not be treated as source of truthOUT_OF_SCOPE:
- Reviving `OptionA.Blazor.Blog.Builder`
- Introducing `OptionA.Blazor.Interactive`
- WYSIWYG editing- Backward-compatibility work beyond the explicit `Post` shimExisting issues **#3** and **#4** remain valid; #4 should explicitly wait for the implementation issues below because the README currently runs ahead of the code.
- Skills Used: refine
- Tools Used: skill- report_intent- view- glob- rg- powershell
- Changed Files: none

## Recent Decisions

(none)