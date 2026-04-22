# Decision 160

- Source: run
- Issue: 46
- Run: 71
- Session: devteam-developer-bf52dacf371b
- Created: 2026-04-22T17:30:55.7201096+00:00

## Title

Run #71 Completed

## Detail

Added/verified the Buttons playground page and descriptor (already present in OptionA.Blazor.Test.Shared). Registered playground services were already present in both hosts. Fixed a compile error in OptionA.Blazor.Server.Test by replacing MapRazorComponents<ServerHostApp>() with MapRazorComponents<App>(). Stopped a running test host process that was locking build output and rebuilt the solution successfully.

## Changed Files

(none)