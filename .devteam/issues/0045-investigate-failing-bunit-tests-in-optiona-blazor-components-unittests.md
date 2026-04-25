# Issue 0045: Investigate failing bUnit tests in OptionA.Blazor.Components.UnitTests

- Status: done
- Role: tester
- Area: components
- Priority: 85
- Depends On: none
- Roadmap Item: 1
- Family: components
- External: none
- Pipeline: 30
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Six tests in OptionA.Blazor.Components.UnitTests are failing with WaitForFailedException/ElementNotFound (Gallery and Carousel suites). Suspected causes: timing/DefaultWaitTimeout too short, DOM selectors changed, or recent status-sync changes altered render behavior. Actions: reproduce locally, increase TestContext.DefaultWaitTimeout for suspects to confirm flakiness, add focused logs/assert probes, and if deterministic, fix component or test. FilesInScope: OptionA.Blazor.Components.UnitTests/**, OptionA.Blazor.Components/**, tests logs. (none)

## Latest Run

- Run: 36
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-tester-992a8c8b4068
- Updated: 2026-04-25T21:37:04.2663953+00:00
- Summary: Reproduced the six failing bUnit tests, inspected test and component code, and fixed root causes:
- OptAComponent: fixed RemovedClasses null-handling (was using invalid [] literal).
- OptAGallery: mark first registered image IsCurrent so a default image is shown.
- OptACarousel: call InvokeAsync(StateHasChanged) after RegisterChild so carousel re-renders when slides register.

Ran tests:
- Ran OptionA.Blazor.Components.UnitTests (88 tests) — all passed.

Changes committed with Co-authored-by trailer.
- Skills Used: debug
- Tools Used: functions.grep (code & log searches), functions.view (opened test & component files), functions.edit (applied source edits), functions.powershell (ran dotnet test and git commit), functions.report_intent (intent logging during tasks)
- Changed Files: none

## Recent Decisions

(none)