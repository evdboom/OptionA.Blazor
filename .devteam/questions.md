# Open questions

## Question 12

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T16:09:23.9984309+00:00

Runs 41-45 all failed with timeout (600s) and 0 tools used — the agent isn't initializing at all, not just taking too long. This affects code-change tasks with gpt-5.4-mini. Should the default model be changed (e.g. back to gpt-5.4), the timeout increased, or is there a known platform issue? Until resolved, only workspace-tool-only tasks will reliably complete.

## Question 13

- Type: blocking
- Status: Open
- Asked: 2026-04-22T16:35:59.1231397+00:00

Total budget is48.94/50 credits committed with only ~1.06 credits remaining. All code-change and architecture work is effectively blocked until the budget cap is raised or reset. Please increase the TotalCreditCap (and PremiumCreditCap as needed) so that the remaining high-priority implementation issues (#78 build break, #10/#24/#38 components, #69/#70 Interactive package) can proceed.

## Question 14

- Type: blocking
- Status: Open
- Asked: 2026-04-22T16:47:19.4754341+00:00

Q#13 remains open: budget is at49.93/50 credits (~0.07 left). The selected batch may consume the last credits. Please raise TotalCreditCap (and PremiumCreditCap as needed) before the next orchestration loop so that the high-value implementation issues (#78build break, #10/#24/#38 playground components, #69/#70 Interactive package) can proceed.

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

