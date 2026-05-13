# Issue 0036: EliminateParameterCoercer duplication with DirectivePlaygroundDescriptor.ConvertValue

- Status: done
- Role: frontend-developer
- Area: blog-document
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: blogdocument
- External: none
- Pipeline: 24
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Both `ParameterCoercer.TryCoerce` and `DirectivePlaygroundDescriptor.ConvertValue` implement identical type-coercion logic (string→string, bool, int, enum). Extract a shared internal `TypeCoercionHelper` used by both call sites. Files in scope: `OptionA.Blazor.Blog/Document/Internal/ParameterCoercer.cs`, `OptionA.Blazor.Blog/Document/Internal/DirectivePlaygroundDescriptor.cs`. Acceptance criteria: (1) Single source of truth for coercion

## Latest Run

- Run: 25
- Status: Completed
- Model: claude-sonnet-4.6
- Session: devteam-frontend-developer-61228213c8b9
- Updated: 2026-05-13T14:17:17.1298687+00:00
- Summary: Extracted a new `TypeCoercionHelper` internal class in `OptionA.Blazor.Blog/Document/Internal/TypeCoercionHelper.cs` with two public methods:
- `TryCoerce(Type, string?)` — handles null-as-bool-shorthand, returns null on failure (used by `ParameterCoercer`)
- `CoerceWithFallback(Type, string)` — returns raw string on failure (used by `DirectivePlaygroundDescriptor`)

Both call sites now delegate to a single private `CoerceNonNull` method for the shared string/bool/int/enum logic. All 210 blog unit tests pass.
- Skills Used: (none)
- Tools Used: view, create, edit, powershell
- Changed Files: none

## Recent Decisions

(none)