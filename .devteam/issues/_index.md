# Issue Index

| ID   | Title | Status | Role | Area | Depends On |
|------|-------|--------|------|------|------------|
| 0001 | Run the planning session and split the work | done | planner | — | — |
| 0002 | Design the technical approach and create execution issues | done | architect | — | 0001 |
| 0003 | Retire OptionA.Blazor.Blog.Builder | done | frontend-developer | — | 0002 |
| 0004 | Update readmes and add worked authoring example | done | docs | — | 0002 |
| 0005 | Add structured document parsing and playground directive rendering | done | frontend-developer | document-rendering | 0002 |
| 0006 | Add whitelisted inline OptA component rendering in OptADocument | done | frontend-developer | document-rendering | 0002 |
| 0007 | Add front-matter metadata and Post shim for OptADocument | open | frontend-developer | document-rendering | 0002 |
| 0008 | Implement the technical approach and create execution issues | done | developer | — | 0002 |
| 0009 | Test the technical approach and create execution issues | done | tester | — | 0008 |
| 0010 | Test Retire OptionA.Blazor.Blog.Builder | blocked | tester | — | 0003 |
| 0011 | Finish MAUI Blog.Builder retirement cleanup | done | frontend-developer | — | 0003 |
| 0012 | Finish retiring Blog.Builder from MAUI test app | done | frontend-developer | — | 0003 |
| 0013 | Test Add structured document parsing and playground directive rendering | done | tester | document-rendering | 0005 |
| 0014 | Test Finish MAUI Blog.Builder retirement cleanup | done | tester | — | 0011 |
| 0015 | Test Finish retiring Blog.Builder from MAUI test app | done | tester | — | 0012 |
| 0016 | Audit recent execution drift | done | auditor | repo-audit | 0008, 0003, 0005, 0011, 0012 |
| 0017 | Test Add whitelisted inline OptA component rendering in OptADocument | open | tester | document-rendering | 0006 |
| 0018 | Fix brittle DevTeam workspace status assertion | done | tester | — | — |
| 0019 | Fix stale DevTeam workspace expectation for issue16 | done | tester | — | — |
| 0020 | Implement Fix stale DevTeam workspace expectation for issue16 | open | developer | — | 0019 |
| 0021 | Implement Fix brittle DevTeam workspace status assertion | open | developer | — | 0018 |
| 0022 | Add fixture-based DevTeam workspace integrity coverage | open | frontend-developer | repo-audit | — |
| 0023 | Realign OptADocument metadata docs to shipped behavior | open | docs | document-rendering | 0007 |
| 0024 | Clarify and harden the OptADocument component-tag contract | open | frontend-developer | document-rendering | 0006 |
| 0025 | Make AddOptionABootstrapBlog truthful and covered by tests | open | fullstack-developer | blog-config | — |
