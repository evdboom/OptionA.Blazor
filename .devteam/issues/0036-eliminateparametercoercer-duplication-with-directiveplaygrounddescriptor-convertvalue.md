# Issue 0036: EliminateParameterCoercer duplication with DirectivePlaygroundDescriptor.ConvertValue

- Status: open
- Role: frontend-developer
- Area: blog-document
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: blog-document
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Both `ParameterCoercer.TryCoerce` and `DirectivePlaygroundDescriptor.ConvertValue` implement identical type-coercion logic (string→string, bool, int, enum). Extract a shared internal `TypeCoercionHelper` used by both call sites. Files in scope: `OptionA.Blazor.Blog/Document/Internal/ParameterCoercer.cs`, `OptionA.Blazor.Blog/Document/Internal/DirectivePlaygroundDescriptor.cs`. Acceptance criteria: (1) Single source of truth for coercion

## Latest Run

(none)

## Recent Decisions

(none)