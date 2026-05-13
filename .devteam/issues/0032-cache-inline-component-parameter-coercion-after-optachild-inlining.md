# Issue 0032: Cache inline component parameter coercion after OptAChild inlining

- Status: open
- Role: frontend-developer
- Area: none
- Priority: 58
- Depends On: 0024
- Roadmap Item: 1
- Family: cacheinlinecomponentparametercoercionafteroptachildinlining
- External: none
- Pipeline: 22
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: move inline-component parameter coercion out of `OptAChild.razor` render-time execution and cache the coerced parameter dictionary for unchanged `InlineComponentContent` inputs. Why: commit `1911c95` replaced the previous `OnParametersSet`-based caching in `OptADocumentComponent` with render-time `ParameterCoercer.Coerce(...)`, which now repeats reflection and allocation on every rerender for each inline component. How: keep `OptADocumentComponent` deleted and `InlineComponentContent` internal, but restore lifecycle-based caching in `OptAChild` or a new internal-only renderer/helper so unchanged content does not re-coerce on rerender. Acceptance criteria: identical rendered output for registered and unregistered inline components

## Latest Run

(none)

## Recent Decisions

(none)