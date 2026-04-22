# Issue 0048: Fix4 failing button E2E tests

- Status: open
- Role: developer
- Area: tests
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: tests
- External: none
- Pipeline: 36
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Supersedes #37(which timed out). Four specific tests fail: Given_ServerApp_When_ClickingShowButton_Then_OptAButtonsAppear, Given_ServerApp_When_ClickingOptAButton_Then_ClickCountIncreases, and WebAssembly equivalents. Diagnose root cause (likely Singleton vs Scoped registration issue noted in #5, or missing Playwright browser install). Fix and verify only these4 tests. Do not expand scope.

## Latest Run

- Run: 34
- Status: Failed
- Model: gpt-5.4
- Session: devteam-developer-0a8be3f134bf
- Updated: 2026-04-22T15:14:39.5848764+00:00
- Summary: Agent timed out after 600 seconds.
- Skills Used: none
- Tools Used: none
- Changed Files: none

## Recent Decisions

- #90 [run] Run #34 Failed: Agent timed out after 600 seconds.