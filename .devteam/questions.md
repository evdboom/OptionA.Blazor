# Open questions

## Question 12

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T16:09:23.9984309+00:00

Runs 41-45 all failed with timeout (600s) and 0 tools used — the agent isn't initializing at all, not just taking too long. This affects code-change tasks with gpt-5.4-mini. Should the default model be changed (e.g. back to gpt-5.4), the timeout increased, or is there a known platform issue? Until resolved, only workspace-tool-only tasks will reliably complete.

## Question 15

- Type: blocking
- Status: Open
- Asked: 2026-04-22T16:50:33.9719747+00:00

Prefer fix-in-place or temporary exclusion: should OptionA.Blazor.Interactive be fixed now (restore missing types/refs) or excluded from solution/test runs until implemented?

## Question 16

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T16:50:33.9719812+00:00

Are the missing types expected to live in another package/repo (should a package reference be added) or were they removed accidentally?

## Question 19

- Type: blocking
- Status: Open
- Asked: 2026-04-22T17:13:50.8536813+00:00

Should OptionA.Blazor.Interactive depend on OptionA.Blazor.Playground (restore/add reference) to obtain PlaygroundDescriptorBase, PlaygroundLayout, PlaygroundOptions, IPlaygroundDataProvider, OptAComponent, and related types — or should Interactive be adapted to a new, decoupled API (please confirm preferred shape)?

## Question 20

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T17:13:50.8537462+00:00

Preferred package metadata values (Authors/Company/Version) beyond the defaults added (OptionA /0.1.0)?

## Question 21

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T17:14:44.7804269+00:00

Should the corrected commands be executed automatically by CI (generate-and-run), or should generation remain manual review + operator-run for safety?

## Question 22

- Type: blocking
- Status: Open
- Asked: 2026-04-22T17:26:27.2289380+00:00

Q#15 and Q#19 remain open: shouldOptionA.Blazor.Interactive be fixed in-place (add Playground reference) or excluded from the solution until types are settled? This blocks all Interactive-area issues (#78, #91, #69, #70, #92).

## Question 23

- Type: blocking
- Status: Open
- Asked: 2026-04-22T17:30:16.2614876+00:00

Should OptionA.Blazor.Interactive be fixed in-place (restore or change its references/types to match Playground) or temporarily removed from the solution/test runs until Interactive/Playground types are settled?

## Question 24

- Type: blocking
- Status: Open
- Asked: 2026-04-22T17:30:16.2614890+00:00

Should Interactive depend on Playground (restore/add reference) to obtain PlaygroundDescriptorBase, PlaygroundLayout, IPlaygroundDataProvider, and other types — or should Interactive be rewritten to a decoupled API and unit tests updated accordingly?

## Question 25

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T17:30:16.2615923+00:00

Preferred approach for resolving parameter-mapping failures: adapt playground runtime to accept arbitrary parameter names (more tolerant) or update unit-test fixtures so test components declare the exact parameter properties used in descriptors?

## Question 26

- Type: blocking
- Status: Open
- Asked: 2026-04-22T17:33:24.3095833+00:00

Q#22/23/24 remain unresolved: shouldOptionA.Blazor.Interactive be fixed in-place (restore/change references to Playground types) or temporarily excluded from the solution until the Interactive/Playground API shape is decided? All high-priority Interactive issues (#78, #91, #69, #70, #92) are gated on this answer.

## Question 27

- Type: blocking
- Status: Open
- Asked: 2026-04-22T17:35:24.9802814+00:00

Preferred delivery: should the prototype be added inside OptionA.Blazor.Storage or published as a new separate package (recommended: separate package to avoid destabilizing current APIs)?

## Question 28

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T17:35:24.9802866+00:00

For migrations: prefer C#-first migration classes (IIndexedDbMigration) applied at runtime, or generate migration scripts from model diffs (more complex)?

## Question 29

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T17:35:24.9802880+00:00

Is browser-only support acceptable (SupportedPlatform=browser already set), or must server-side usage (via server-side prerendering) be explicitly supported?

## Question 30

- Type: blocking
- Status: Open
- Asked: 2026-04-22T17:35:38.1380342+00:00

Should the new Interactive package depend on the existing Playground types (reuse PlaygroundDescriptorBase, PlaygroundLayout, IPlaygroundDataProvider) or should both Playground and Interactive depend on a new small shared contracts package (preferred for decoupling)? This affects extraction order and adapter work.

## Question 31

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T17:35:38.1380387+00:00

Confirm preferred package names: OptionA.Blazor.Builder.Core (or OptionA.Blazor.ContentModel) for models/services, OptionA.Blazor.Interactive for live editing, and OptionA.Blazor.Editor for Monaco — acceptable?

## Question 32

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T17:35:38.1381001+00:00

Should Monaco/editor integration be included in the Interactive package by default or provided as an optional separate package (to keep runtime lean)?

## Question 33

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T17:37:10.6703000+00:00

Prefer local/script-level mitigation (always run devteam export after edits) or pursue upstream change to the devteam CLI to re-export automatically after mutating operations?

