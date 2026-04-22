# Issue 0129: Extract blog content model and services into Builder.Core

- Status: open
- Role: developer
- Area: interactive-migration
- Priority: 90
- Depends On: none
- Roadmap Item: 1
- Family: interactive-migration
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Create a new package OptionA.Blazor.Builder.Core (or OptionA.Blazor.ContentModel) containing Post, Content-part models, IBuilderService interface and minimal helpers. Update Blog.Builder and other projects to reference the new package. Add unit tests for models and serialization.

## Latest Run

(none)

## Recent Decisions

(none)