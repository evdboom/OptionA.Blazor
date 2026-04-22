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

