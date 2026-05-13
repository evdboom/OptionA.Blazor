# Issue 0031: Audit recent execution drift

- Status: done
- Role: auditor
- Area: repo-audit
- Priority: 61
- Depends On: 0024, 0017, 0026
- Roadmap Item: 1
- Family: repoaudit
- External: none
- Pipeline: 21
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Guardrail audit triggered by scheduled guardrail cadence. Recent implementation runs since last audit: 3. Assess auditable/testable/maintainable drift and create focused remediation issues.

## Latest Run

- Run: 23
- Status: Completed
- Model: claude-opus-4.6
- Session: devteam-auditor-da06719cfb7d
- Updated: 2026-05-13T14:11:03.8224110+00:00
- Summary: Audited 149 recently changed files (8952 insertions since main). The new Document pipeline (`OptADocument`, `MarkdownDocumentParser`, internal collaborators) and Playground registry are well-structured with good separation of concerns and constructor injection. Tests are comprehensive for happy paths and cover most error paths. Key findings:

**Recent drift (introduced by AI iterations):**
1. **`DocumentMetadata.ParseFromMarkdown` is a static method with parsing logic embedded in a publicDTO** — mixes data and parsing, untestable in isolation without constructing the full string, violates single-responsibility.
2. **`OptAChild.razor` now has casting + conditional logic for two `internal` content types** — the playground/inline-component branches directly reference `Internal` namespace types and call `ParameterCoercer.Coerce` inline, coupling the shared renderer to document-pipeline internals.
3. **`PlaygroundDirectivePreprocessor` uses non-compiled `Regex.Match`** at lines 90/98 — regex is re-compiled on every call; should use `[GeneratedRegex]` like `InlineComponentTagParser` does.
4. **`DocumentComponentRegistry` is not thread-safe** — uses `Dictionary<>` unlike `PlaygroundRegistry` which uses `ConcurrentDictionary`. Registration at startup is safe, but if a consumer adds components after first request, this silently corrupts.
5. **Duplicate conversion logic** — `DirectivePlaygroundDescriptor.ConvertValue` and `ParameterCoercer.TryCoerce` implement the same type coercion (string→bool/int/enum) independently. DRY violation and divergence risk.

**Test coverage gaps:**
- No test for `DocumentMetadata.ParseFromMarkdown` with malformed YAML (multi-line values, missing closing `---`).
- No thread-safety test for `PlaygroundRegistry` (concurrent Register/TryGet).
- `PlaygroundDirectivePreprocessor` has no direct unit tests — only exercised indirectly via parser integration tests.
- Skills Used: scout, hygiene
- Tools Used: git, grep, view, powershell, task (analyst, explore agents — though they failed due to permission issues)
- Changed Files: none

## Recent Decisions

(none)