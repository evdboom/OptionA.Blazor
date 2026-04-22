# Issue 0018: Add browser coverage for playground interactions

- Status: open
- Role: tester
- Area: playground-host
- Priority: 78
- Depends On: 0015
- Roadmap Item: 1
- Family: playgroundhost
- External: none
- Pipeline: 16
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Extend `tests\OptionA.Blazor.E2E` with a dedicated page object and WebAssembly/Server tests for the new playground pages. Add `tests\OptionA.Blazor.E2E\PageObjects\PlaygroundPage.cs` and matching test files under `tests\OptionA.Blazor.E2E\Tests\`. Assert that the playground page loads, the preview renders the expected component root attribute (`[opta-button]` or `[opta-tabs]`), changing a text/boolean/select editor updates preview state, and the generated code block changes from the default snippet after the edit. Keep direct Playwright calls inside page objects and prefer awaited DOM assertions over timing-only waits.

## Latest Run

(none)

## Recent Decisions

(none)