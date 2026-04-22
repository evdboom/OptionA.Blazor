# Open questions

## Question 5

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T08:06:22.4976126+00:00

#24 ("Implement OptAPlayground container component") depends on #9 which was already verified fully done in Run #21. When the agent runs #24 it will almost certainly just verify and close. Should we proactively close #24 now and save the credit, or let the agent confirm so the pipeline has an explicit done record?

## Question 6

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T08:12:38.5080385+00:00

#10 (OptAPlaygroundPreview) and #12 (OptAPlaygroundCode) are both in `playground-components` and can't run alongside #11. Should the next batch after this one pair #10 + #12 together (same area, different files — `Preview.razor` vs `Code.razor`), or should they run sequentially to reduce merge risk?

## Question 7

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T08:28:59.9949119+00:00

Q#5 answered: include #24 in this batch so the pipeline has an explicit done record; proactive close would save one credit but lose audit trail — not worth it.

## Question 8

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T08:28:59.9949124+00:00

Q#6 answered: pair #10 + #12 together in the batch immediately after #11 completes (same area, different files — `Preview.razor` vs `Code.razor` — low merge risk; running them together saves one iteration).

## Question 9

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T08:43:07.7910866+00:00

Issue #11 (OptAPlaygroundEditor) has timed out twice at600 s. The Editors/ folder already exists with all component stubs. Should the next attempt increase the timeout budget, or should #11 be split into smaller issues (e.g., separate issues for the container editor and each individual OptAEditor* component)?

## Question 10

- Type: non-blocking
- Status: Open
- Asked: 2026-04-22T08:59:08.6165505+00:00

Q#9 answered: split #11 into two focused issues — "Implement OptAPlaygroundEditor container component" and "Implement individual OptAEditor* components" — see new ISSUES above. Running them separately eliminates the 600s timeout risk. Issues #28 and #32 (currently blocked on #11) should have their dependency updated to the new split issues once the supersession is recorded.

