# Issue 0024: Inline OptADocumentPlayground/OptADocumentComponent into OptAChild and internalize content types

- Status: open
- Role: frontend-developer
- Area: none
- Priority: 75
- Depends On: 0023
- Roadmap Item: 1
- Family: inlineoptadocumentplaygroundoptadocumentcomponentintooptachildandinternalizecontenttypes
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

What: (1) Extract CoerceParameters/TryCoerce from OptADocumentComponent.razor.cs into a new internal static class ParameterCoercer in Document/Internal/ParameterCoercer.cs. (2) Delete OptADocumentPlayground.razor + .razor.cs and OptADocumentComponent.razor + .razor.cs. (3) In OptAChild.razor, replace the Playground case with inline rendering: cast to PlaygroundDirectiveContent, if ResolvedDescriptor is not null render <OptAPlayground Descriptor="..."/>, else if ErrorMessage render the error div with class opta-playground-error and role=alert. Replace the InlineComponent case with inline rendering: cast to InlineComponentContent, if ComponentType is not null call ParameterCoercer.Coerce() and render <DynamicComponent Type="..." Parameters="..."/>, else render warning span with class opta-document-component-warning. (4) Change PlaygroundDirectiveContent.cs from public sealed to internal sealed. (5) Change InlineComponentContent.cs from public sealed to internal sealed. Acceptance criteria: project builds with no warnings

## Latest Run

(none)

## Recent Decisions

(none)