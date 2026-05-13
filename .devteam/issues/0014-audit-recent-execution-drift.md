# Issue 0014: Audit recent execution drift

- Status: done
- Role: auditor
- Area: repo-audit
- Priority: 77
- Depends On: 0003, 0008, 0006
- Roadmap Item: 1
- Family: repoaudit
- External: none
- Pipeline: 9
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Guardrail audit triggered by scheduled guardrail cadence. Recent implementation runs since last audit: 3. Assess auditable/testable/maintainable drift and create focused remediation issues.

## Latest Run

- Run: 9
- Status: Completed
- Model: claude-opus-4.6
- Session: devteam-auditor-5d56b3b62da4
- Updated: 2026-05-13T13:40:57.9707325+00:00
- Summary: Performed a full ATM (Auditable / Testable / Maintainable) audit across the 3 recent execution runs (playground registry, OptADocument renderer, directive/inline-component/front-matter support). Identified **6 focused drift issues** spanning recent implementation:

**Key findings:**

1. **DocumentMetadata style drift** — `DocumentMetadata.ParseFromMarkdown` is a50-line static method with inline YAML parsing, no XML doc comments, brace-style deviations, and explicit `using System` imports (rest of codebase uses implicit usings). This was clearly AI-generated in a different style pass than the surrounding code.

2. **Fire-and-forget EventCallback in OnParametersSet** — `OptADocument.razor.cs` line 39 does `_ = OnMetadataParsed.InvokeAsync(metadata)` inside a synchronous lifecycle method. This silently swallows exceptions from async callbacks; it should be `await`ed via `OnParametersSetAsync`.

3. **Duplicated test accessor types** — `MarkdownDocumentParserAccessor` is copy-pasted identically across `MarkdownDocumentParserTests.cs` and `InlineComponentTests.cs`. A shared test helper would prevent them drifting apart.

4. **AllowUnsafeBlocks in Blog.csproj** — The Blog project enables `AllowUnsafeBlocks` but nothing in the Markdown pipeline or Blog components uses unsafe code. This was likely cargo-culted from the Helpers project.

5. **PostHelpers missing XML doc comments** — `PostHelpers.cs` is a new public class with zero XML documentation, violating the CS1591-as-error policy set in `Directory.Build.props` for packable projects.

6. **DocumentComponentRegistry thread safety gap** — `PlaygroundRegistry` uses `ConcurrentDictionary` for thread safety, but the sibling `DocumentComponentRegistry` uses a plain `Dictionary`. Both are singleton registries populated at startup and queried at render time. Though startup-only writes are likely safe in practice, the inconsistency is a maintenance trap.
- Skills Used: none
- Tools Used: view, grep, powershell (git log, git diff), task (explore agents — failed due to permission issues, fell back to direct tools)
- Changed Files: none

## Recent Decisions

(none)