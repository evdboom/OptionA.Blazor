# Issue Index

| ID   | Title | Status | Role | Area | Depends On |
|------|-------|--------|------|------|------------|
| 0001 | Run the planning session and split the work | done | planner | — | — |
| 0002 | Design the technical approach and create execution issues | done | architect | — | 0001 |
| 0003 | Fix OptADocumentPlayground build error (CS0262 / CS0053) | done | frontend-developer | — | — |
| 0004 | RemoveOptionA.Blazor.Blog.Builder from OptionA.Blazor.sln | done | frontend-developer | — | — |
| 0005 | Update OptionA.Blazor.Blog/readme.md to cover OptADocument and Markdown authoring | open | docs | — | — |
| 0006 | Remove Blog.Builder references from solution and test project | done | frontend-developer | — | — |
| 0007 | Fix stale documentation in Blog readme claiming features are planned | done | docs | — | — |
| 0008 | Implement the technical approach and create execution issues | done | developer | — | 0002 |
| 0009 | Test Remove Blog.Builder references from solution and test project | open | tester | — | 0006 |
| 0010 | Test Fix OptADocumentPlayground build error (CS0262 / CS0053) | done | tester | — | 0003 |
| 0011 | Review Fix OptADocumentPlayground build error (CS0262 / CS0053) | done | reviewer | — | 0003 |
| 0012 | Test the technical approach and create execution issues | done | tester | — | 0008 |
| 0013 | Review Implement the technical approach and create execution issues | done | reviewer | — | 0008 |
| 0014 | Audit recent execution drift | done | auditor | repo-audit | 0003, 0008, 0006 |
| 0015 | Test RemoveOptionA.Blazor.Blog.Builder from OptionA.Blazor.sln | open | tester | — | 0004 |
| 0016 | Review RemoveOptionA.Blazor.Blog.Builder from OptionA.Blazor.sln | open | reviewer | — | 0004 |
| 0017 | Fix fire-and-forget EventCallback in OptADocument.OnParametersSet | done | frontend-developer | blog-document | — |
| 0018 | Normalize DocumentMetadata coding style to repo conventions | open | frontend-developer | blog-document | — |
| 0019 | Add XML doc comments to PostHelpers public API | open | fullstack-developer | blog-document | — |
| 0020 | Extract sharedMarkdownDocumentParserAccessor test helper to eliminate duplication | open | developer | blog-tests | — |
| 0021 | Remove unnecessary AllowUnsafeBlocks from Blog.csproj | open | frontend-developer | blog | — |
| 0022 | Align DocumentComponentRegistry thread safety with PlaygroundRegistry | open | developer | blog-document | — |
| 0023 | Define and contain OptADocument helper API surface | done | architect | — | 0003 |
| 0024 | Inline OptADocumentPlayground/OptADocumentComponent into OptAChild and internalize content types | done | frontend-developer | — | 0023 |
| 0025 | Migrate OptADocumentPlayground and OptADocumentComponent unit tests after internalization | done | tester | — | 0023 |
| 0026 | Implement Define and contain OptADocument helper API surface | done | developer | — | 0023 |
| 0027 | Test Fix fire-and-forget EventCallback in OptADocument.OnParametersSet | done | tester | blog-document | 0017 |
| 0028 | Test Inline OptADocumentPlayground/OptADocumentComponent into OptAChild and internalize content types | done | tester | — | 0024 |
| 0029 | Review Inline OptADocumentPlayground/OptADocumentComponent into OptAChild and internalize content types | done | reviewer | — | 0024 |
| 0030 | Test Define and contain OptADocument helper API surface | blocked | tester | — | 0026 |
| 0031 | Audit recent execution drift | done | auditor | repo-audit | 0024, 0017, 0026 |
| 0032 | Cache inline component parameter coercion after OptAChild inlining | open | frontend-developer | — | 0024 |
| 0033 | Implement Migrate OptADocumentPlayground and OptADocumentComponent unit tests after internalization | done | developer | — | 0025 |
| 0034 | Test Migrate OptADocumentPlayground and OptADocumentComponent unit tests after internalization | open | tester | — | 0033 |
| 0035 | Extract DocumentMetadata parsing into injectable service | open | fullstack-developer | blog-document | — |
| 0036 | EliminateParameterCoercer duplication with DirectivePlaygroundDescriptor.ConvertValue | open | frontend-developer | blog-document | — |
| 0037 | Use GeneratedRegex in PlaygroundDirectivePreprocessor | open | frontend-developer | blog-document | — |
| 0038 | Add direct unit tests for PlaygroundDirectivePreprocessor | open | tester | blog-document | — |
| 0039 | Add DocumentMetadata front-matter edge-case tests | open | tester | blog-document | — |
| 0040 | Make DocumentComponentRegistry thread-safe | open | frontend-developer | blog-document | — |
| 0041 | Reconcile OptADocument metadata callback behavior with tests | open | frontend-developer | document-metadata | — |
