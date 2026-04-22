# Issue 0087: Fix persistence of superseded status for issue #16 in authoritative .devteam workspace state

- Status: open
- Role: developer
- Area: devteam
- Priority: 90
- Depends On: 0036
- Roadmap Item: 1
- Family: devteam
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

When issue #16 was marked Done by the DevTeam CLI, .devteam\state\issues.json still records Status: "Open" and the issue markdown shows "- Status: open". Reproduce by running the added test (OptionA.Blazor.DevTeam.Tests). Acceptance: tests pass (issue16 shows Status: "Done" in both .devteam\state\issues.json and .devteam\issues\0016-*.md), add regression test to CI.

## Latest Run

(none)

## Recent Decisions

(none)