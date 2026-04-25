# Issue Index

| ID   | Title | Status | Role | Area | Depends On |
|------|-------|--------|------|------|------------|
| 0001 | Run the planning session and split the work | done | planner | — | — |
| 0002 | Design the technical approach and create execution issues | done | architect | — | 0001 |
| 0003 | Retire OptionA.Blazor.Blog.Builder | done | frontend-developer | — | 0002 |
| 0004 | Update readmes and add worked authoring example | done | docs | — | 0002 |
| 0005 | Add structured document parsing and playground directive rendering | done | frontend-developer | document-rendering | 0002 |
| 0006 | Add whitelisted inline OptA component rendering in OptADocument | open | frontend-developer | document-rendering | 0002 |
| 0007 | Add front-matter metadata and Post shim for OptADocument | open | frontend-developer | document-rendering | 0002 |
| 0008 | Implement the technical approach and create execution issues | done | developer | — | 0002 |
| 0009 | Test the technical approach and create execution issues | done | tester | — | 0008 |
| 0010 | Test Retire OptionA.Blazor.Blog.Builder | blocked | tester | — | 0003 |
| 0011 | Finish MAUI Blog.Builder retirement cleanup | open | frontend-developer | — | 0003 |
| 0012 | Finish retiring Blog.Builder from MAUI test app | open | frontend-developer | — | 0003 |
| 0013 | Test Add structured document parsing and playground directive rendering | open | tester | document-rendering | 0005 |
