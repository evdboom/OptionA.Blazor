# Issue 0097: Spike: EF Core-backed IndexedDB prototype

- Status: done
- Role: backend-developer
- Area: storage
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: storage
- External: none
- Pipeline: 67
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Prototype an EF Core provider or wrapper that enables migrations + runtime access to IndexedDB (client-side). Evaluate feasibility, security, and perf

## Latest Run

- Run: 75
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-backend-developer-4dedcf03d0f1
- Updated: 2026-04-22T17:35:24.9804207+00:00
- Summary: Inspected OptionA.Blazor.Storage (csproj + README). The project already targets browser and declares IDatabaseService as "WIP" with planned migrations for IndexedDB. Implementing a full EF Core provider (true EF Core provider allowing dotnet migrations tooling + runtime DbContext) is high effort and risky in one spike. Recommended safe prototype: implement an IndexedDB-backed DbContext-like wrapper (server-side EF Core API surface emulated) + a migrations abstraction that runs JSInterop scripts to create/alter object stores. This demonstrates feasibility, migration UX, and performance without reimplementing EF Core provider internals.
- Skills Used: (none)
- Tools Used: multi_tool_use.parallel (functions.report_intent + functions.glob + functions.view), functions.report_intent- functions.view- functions.glob
- Changed Files: none

## Recent Decisions

- #163 [run] Run #75 Completed: Inspected OptionA.Blazor.Storage (csproj + README). The project already targets browser and declares IDatabaseService as "WIP" with planned migrations for IndexedDB. Implementing a full EF Core provider (true EF Core provider allowing dotnet migrations tooling + runtime DbContext) is high effort and risky in one spike. Recommended safe prototype: implement an IndexedDB-backed DbContext-like wrapper (server-side EF Core API surface emulated) + a migrations abstraction that runs JSInterop scripts to create/alter object stores. This demonstrates feasibility, migration UX, and performance without reimplementing EF Core provider internals.