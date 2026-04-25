# Issue Index

| ID   | Title | Status | Role | Area | Depends On |
|------|-------|--------|------|------|------------|
| 0001 | Run the planning session and split the work | done | planner | — | — |
| 0002 | Design the technical approach and create execution issues | done | architect | — | 0001 |
| 0003 | Retire OptionA.Blazor.Blog.Builder | done | frontend-developer | — | 0002 |
| 0004 | Update readmes and add worked authoring example | done | docs | — | 0002 |
| 0005 | Add structured document parsing and playground directive rendering | done | frontend-developer | document-rendering | 0002 |
| 0006 | Add whitelisted inline OptA component rendering in OptADocument | done | frontend-developer | document-rendering | 0002 |
| 0007 | Add front-matter metadata and Post shim for OptADocument | done | frontend-developer | document-rendering | 0002 |
| 0008 | Implement the technical approach and create execution issues | done | developer | — | 0002 |
| 0009 | Test the technical approach and create execution issues | done | tester | — | 0008 |
| 0010 | Test Retire OptionA.Blazor.Blog.Builder | blocked | tester | — | 0003 |
| 0011 | Finish MAUI Blog.Builder retirement cleanup | done | frontend-developer | — | 0003 |
| 0012 | Finish retiring Blog.Builder from MAUI test app | done | frontend-developer | — | 0003 |
| 0013 | Test Add structured document parsing and playground directive rendering | done | tester | document-rendering | 0005 |
| 0014 | Test Finish MAUI Blog.Builder retirement cleanup | done | tester | — | 0011 |
| 0015 | Test Finish retiring Blog.Builder from MAUI test app | done | tester | — | 0012 |
| 0016 | Audit recent execution drift | done | auditor | repo-audit | 0008, 0003, 0005, 0011, 0012 |
| 0017 | Test Add whitelisted inline OptA component rendering in OptADocument | done | tester | document-rendering | 0006 |
| 0018 | Fix brittle DevTeam workspace status assertion | done | tester | — | — |
| 0019 | Fix stale DevTeam workspace expectation for issue16 | done | tester | — | — |
| 0020 | Implement Fix stale DevTeam workspace expectation for issue16 | done | developer | — | 0019 |
| 0021 | Implement Fix brittle DevTeam workspace status assertion | open | developer | — | 0018 |
| 0022 | Add fixture-based DevTeam workspace integrity coverage | done | frontend-developer | repo-audit | — |
| 0023 | Realign OptADocument metadata docs to shipped behavior | done | docs | document-rendering | 0007 |
| 0024 | Clarify and harden the OptADocument component-tag contract | open | frontend-developer | document-rendering | 0006 |
| 0025 | Make AddOptionABootstrapBlog truthful and covered by tests | done | fullstack-developer | blog-config | — |
| 0026 | Test Make AddOptionABootstrapBlog truthful and covered by tests | done | tester | blog-config | 0025 |
| 0027 | Close superseded issues20 and 21 (implement-fix follow-ons for live-workspace tests) | open | tester | repo-audit | 0022 |
| 0028 | Test Add fixture-based DevTeam workspace integrity coverage | done | tester | repo-audit | 0022 |
| 0029 | Run and verify CI pipeline-local tests after status-sync implementation | done | frontend-developer | devteam | 0020 |
| 0030 | Test Fix stale DevTeam workspace expectation for issue16 | open | tester | — | 0020 |
| 0031 | Add robust tests for front-matter parsing and OnMetadataParsed | open | tester | document-rendering | 0007 |
| 0032 | Document OptADocument front-matter API | done | docs | documentation | 0007 |
| 0033 | Test Add front-matter metadata and Post shim for OptADocument | open | tester | document-rendering | 0007 |
| 0034 | Review Add front-matter metadata and Post shim for OptADocument | done | reviewer | document-rendering | 0007 |
| 0035 | Test Run and verify CI pipeline-local tests after status-sync implementation | done | tester | devteam | 0029 |
| 0036 | Audit recent execution drift | done | auditor | repo-audit | 0022, 0025, 0007, 0020, 0029 |
| 0037 | Add worked example Markdown to match README | open | docs | document-rendering | — |
| 0038 | Fix OptADocumentPlayground accessibility and related compile errors | done | developer | document-rendering | 0034 |
| 0039 | Add bUnit tests for OptADocument front-matter parsing and Post shim | open | tester | document-rendering | 0034 |
| 0040 | Document OptADocument front-matter and Post shim usage | open | docs | document-rendering | 0034 |
| 0041 | Resolve workspace-state contradictions (Done vs InProgress runs) | done | backend-developer | repo-audit | 0018, 0019 |
| 0042 | Remove stale open questions referencing already-done issues (#3 and #8) | open | developer | repo-audit | 0019, 0020 |
| 0043 | Harden Inline component-tag parser for quoted attribute edge-cases | done | frontend-developer | blog | 0004, 0005 |
| 0044 | Align OptionA.Blazor.Blog README and OptADocument docs with shipped behavior | open | docs | docs | 0034, 0033 |
| 0045 | Investigate failing bUnit tests in OptionA.Blazor.Components.UnitTests | done | tester | components | — |
| 0046 | Selected execution batch: Issues 38, 45, 41, 33 | done | orchestrator | none | — |
| 0047 | Refinement: implement deterministic reconciliation of issues.json vs runs.json | done | backend-developer | repo-audit | 0041 |
| 0048 | Test Resolve workspace-state contradictions (Done vs InProgress runs) | open | tester | repo-audit | 0041 |
| 0049 | Implement Fix OptADocumentPlayground accessibility and related compile errors | done | developer | document-rendering | 0038 |
| 0050 | Implement Investigate failing bUnit tests in OptionA.Blazor.Components.UnitTests | done | developer | components | 0045 |
| 0051 | Test Investigate failing bUnit tests in OptionA.Blazor.Components.UnitTests | done | tester | components | 0050 |
| 0052 | Integrate workspace reconciliation into CI and add tests | open | backend-developer | repo-audit | — |
| 0053 | Test Refinement: implement deterministic reconciliation of issues.json vs runs.json | open | tester | repo-audit | 0047 |
| 0054 | Test Fix OptADocumentPlayground accessibility and related compile errors | open | tester | document-rendering | 0049 |
| 0055 | Audit recent execution drift | done | auditor | repo-audit | 0038, 0041, 0049, 0050, 0047 |
| 0056 | Verify and protect .devteam runtime integrity | done | auditor | repo-audit | — |
| 0057 | Fix OptionA.Blazor.Blog compile errors (OptADocumentPlayground & PlaygroundDirectiveContent) | done | fullstack-developer | document-rendering | 0034 |
| 0058 | Add bUnit tests for OptADocument front-matter, directives, and inline components | open | tester | document-rendering | 0034 |
| 0059 | Sync documentation with implemented behavior and mark experimental features | open | docs | document-rendering | 0034 |
| 0060 | Improve traceability of execution runs and artifacts | open | auditor | repo-audit | — |
| 0061 | Decide Markdown/Markdig handling for escaped-quote HTML blocks | done | frontend-developer | blog | — |
| 0062 | Test Harden Inline component-tag parser for quoted attribute edge-cases | open | tester | blog | 0043 |
| 0063 | Handle Markdig-escaped-quote OptA HTML blocks | open | frontend-developer | blog | 0061 |
| 0064 | Test Decide Markdown/Markdig handling for escaped-quote HTML blocks | open | tester | blog | 0061 |
| 0065 | Update Blog unit tests for bUnit migration | open | backend-developer | document-rendering | 0057 |
| 0066 | Test Fix OptionA.Blazor.Blog compile errors (OptADocumentPlayground & PlaygroundDirectiveContent) | done | tester | document-rendering | 0057 |
| 0067 | Declare and document .devteam authoritative policy | done | auditor | repo-audit | 0056 |
| 0068 | Add deterministic reconciliation script and backups for .devteam state | done | developer | repo-automation | 0056 |
| 0069 | Add CI integrity checks and automated backups for .devteam | open | devops | ci | 0056 |
| 0070 | Implement .devteam authoritative persistence policy and docs | open | developer | repo-audit | 0067 |
| 0071 | Update obsolete bUnit RenderComponent usages to Render | open | fullstack-developer | document-rendering | — |
| 0072 | Run reconcile script in CI (dry-run + validate) | open | devops | ci | 0068 |
| 0073 | Implement Add deterministic reconciliation script and backups for .devteam state | open | developer | repo-automation | 0068 |
