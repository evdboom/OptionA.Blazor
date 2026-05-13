# Issue 0048: Harden directive playground override coercion for typed parameters

- Status: open
- Role: fullstack-developer
- Area: blog-document
- Priority: 79
- Depends On: 0036
- Roadmap Item: 1
- Family: blog-document
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

What: make Markdown playground directive overrides use the same typed-coercion rules as inline component attributes, including nullable unwrapping, and stop storing raw string fallbacks for non-string parameter types. Add an explicit author-visible error path when an override cannot be converted to the descriptor parameter type. Why: `DirectivePlaygroundDescriptor.ConvertValue` currently calls `TypeCoercionHelper.CoerceWithFallback(p.ValueType, raw)`, so `Nullable<T>` parameters do not coerce correctly and invalid `bool/int/enum` overrides degrade to `string`, which can later break editor state or `DynamicComponent` binding at render time. How: centralize coercion behind one helper API that accepts nullable target types consistently, have directive override application return either a correctly typed value or a structured validation failure, and thread those failures into the existing visible document error/warning surface instead of silently keeping the raw string. Add focused tests for `DirectivePlaygroundDescriptor`/document rendering covering nullable bool/int/enum, invalid typed overrides, and parity with `ParameterCoercer` on supported types. AcceptanceCriteria:1. `int?`, `bool?`, and nullable enum playground overrides from directives are applied as typed values.2. Invalid typed overrides do not get stored as `string` defaults for non-string parameters.3. Invalid overrides produce a visible non-fatal document/playground error instead of a render-time binding failure.4. Tests cover string, bool, int, enum, nullable variants, and invalid input cases. FilesInScope: `OptionA.Blazor.Blog\\Document\\Internal\\DirectivePlaygroundDescriptor.cs`, `OptionA.Blazor.Blog\\Document\\Internal\\TypeCoercionHelper.cs`, `OptionA.Blazor.Blog\\Document\\MarkdownDocumentParser.cs`, `OptionA.Blazor.Blog.UnitTests\\Document\\MarkdownDocumentParserTests.cs`, `OptionA.Blazor.Blog.UnitTests\\Document\\OptADocumentTests.cs`, and any closely related document error-rendering helpers. LinkedDecisions: reuse shared coercion logic from issue #36

## Latest Run

(none)

## Recent Decisions

(none)