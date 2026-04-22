# Issue Index

| ID   | Title | Status | Role | Area | Depends On |
|------|-------|--------|------|------|------------|
| 0001 | Run the planning session and split the work | done | planner | — | — |
| 0002 | Design the technical approach and create execution issues | done | architect | — | 0001 |
| 0003 | Assess Blog and Blog Builder packages for future direction | done | analyst | blog | — |
| 0005 | Expand Playwright E2E and bUnit test coverage to all components | open | tester | tests | 0002 |
| 0006 | Implement EF-like entity and query layer for IndexedDB | open | developer | storage | — |
| 0007 | Scaffold Playground project and core model | done | developer | playground-core | — |
| 0008 | Scaffold Playground unit test project | done | developer | playground-core | — |
| 0009 | Implement OptAPlayground container component | done | developer | playground-components | — |
| 0010 | Implement OptAPlaygroundPreview component | open | developer | playground-components | — |
| 0011 | Implement OptAPlaygroundEditor and individual editor components | open | developer | playground-components | — |
| 0012 | Implement OptAPlaygroundCode component | open | developer | playground-components | — |
| 0013 | Implement playground CSS and Bootstrap defaults | open | developer | playground-css | — |
| 0014 | Write comprehensive unit tests for Playground | open | tester | playground-core | — |
| 0015 | Integrate Playground into CI and documentation | open | developer | playground-ci | — |
| 0016 | Implement the technical approach and create execution issues | open | developer | — | 0002 |
| 0017 | Expose playground examples in shared host apps | open | developer | playground-host | 0007, 0009, 0010, 0011, 0012, 0013 |
| 0018 | Add browser coverage for playground interactions | open | tester | playground-host | 0015 |
| 0019 | Implement Scaffold Playground project and core model | open | developer | playground-core | 0007 |
| 0020 | Close superseded issue #19 — PlaygroundDescriptorBase covered by #9 | done | developer | playground-core | — |
| 0021 | Implement Scaffold Playground unit test project | open | developer | playground-core | 0008 |
| 0022 | Scout codebase for: OptAPlaygroundEditor and individual editor components | done | navigator | playground-components | — |
| 0023 | Implement Close superseded issue #19 — PlaygroundDescriptorBase covered by #9 | done | developer | playground-core | 0020 |
| 0024 | Implement OptAPlayground container component | open | developer | playground-components | 0009 |
| 0025 | Close superseded issue #16 — technical approach already delivered by scaffold and component issues | done | developer | — | — |
| 0026 | Test Close superseded issue #19 — PlaygroundDescriptorBase covered by #9 | open | tester | playground-core | 0023 |
| 0027 | Implement Close superseded issue #16 — technical approach already delivered by scaffold and component issues | done | developer | — | 0025 |
| 0028 | Fix OptAPlaygroundEditorTests compile failure on missing IElement type | open | developer | playground-components | 0011 |
| 0029 | Mark #19 Done — superseded by scaffold issue #7 | done | developer | playground-core | — |
| 0030 | Mark #21 Done — superseded by test scaffold issue #8 | done | developer | playground-core | — |
| 0031 | Test Close superseded issue #16 — technical approach already delivered by scaffold and component issues | open | tester | — | 0027 |
| 0032 | Fix OptAPlaygroundEditorTests compile failures | open | developer | playground-components | 0011 |
| 0033 | Fix broken playground editor tests | done | developer | playground-components | — |
| 0034 | Implement Fix broken playground editor tests | done | developer | playground-components | 0033 |
| 0035 | Implement Mark #19 Done — superseded by scaffold issue #7 | done | developer | playground-core | 0029 |
| 0036 | Persist superseded status for issue #16 in authoritative workspace state | done | developer | — | 0027 |
| 0037 | Fix button interaction E2E regressions in Server and WebAssembly samples | open | developer | tests | — |
| 0038 | Implement OptAPlaygroundEditor container component | open | developer | playground-components | — |
| 0039 | Implement individual OptAEditor* editor components in Playground | open | developer | playground-components | — |
| 0040 | Mark #11 superseded — replaced by container and editors split | done | developer | playground-components | — |
| 0041 | Implement Persist superseded status for issue #16 in authoritative workspace state | done | developer | — | 0036 |
| 0042 | Implement Mark #21 Done — superseded by test scaffold issue #8 | open | developer | playground-core | 0030 |
| 0043 | Test Fix broken playground editor tests | open | tester | playground-components | 0034 |
| 0044 | Batch-close 18 stale/completed issues | done | developer | — | — |
| 0045 | Implement PlaygroundCodeGenerator helper | done | developer | playground-components | — |
| 0046 | Add Buttons playground page to test hosts | open | developer | playground-host | — |
| 0047 | Add Tabs playground page to test hosts | open | developer | playground-host | — |
| 0048 | Fix4 failing button E2E tests | open | developer | tests | — |
| 0049 | Add bUnit tests for Carousel and Gallery components | open | tester | tests | — |
| 0050 | Add bUnit tests for Menu and Modal components | open | tester | tests | — |
| 0051 | Add bUnit tests for MessageBox and Responsive components | open | tester | tests | — |
| 0052 | Add bUnit tests for Splitter and Tabs components | open | tester | tests | — |
| 0053 | Add E2E tests for remaining component pages | open | tester | tests | — |
| 0054 | Design EF-inspired IndexedDB API surface | done | architect | storage | — |
| 0055 | Integrate Playground into CI and README | done | developer | playground-ci | — |
| 0056 | Implement typed IndexedDB contracts and model builder | open | developer | storage | 0054 |
| 0057 | Implement model-to-migration adapter on existing IndexedDB migrations | open | developer | storage | 0054 |
| 0058 | Implement IndexedDbContext CRUD and constrained query pipeline | open | developer | storage | 0054 |
| 0059 | Add test coverage for typed IndexedDB context and migrations | open | tester | storage | 0054 |
| 0060 | Implement EF-inspired IndexedDB API surface | open | developer | storage | 0054 |
| 0061 | Implement Integrate Playground into CI and README | open | developer | playground-ci | 0055 |
| 0062 | Implement Batch-close 18 stale/completed issues | done | developer | — | 0044 |
| 0063 | Implement PlaygroundCodeGenerator helper | done | developer | playground-components | 0045 |
| 0064 | Close stale pipeline-generated issues | done | developer | — | — |
| 0065 | Design OptionA.Blazor.Helpers package extraction | done | architect | interactive | — |
| 0066 | Plan OptionA.Blazor.Interactive package scope and design | done | architect | interactive | — |
| 0067 | Add bUnit tests to Blog display components | done | developer | blog | — |
| 0068 | Add deprecation markers to Blog.Builder WYSIWYG builders | done | developer | blog | — |
| 0069 | Implement OptionA.Blazor.Helpers package extraction | open | developer | interactive | 0065 |
| 0070 | Implement OptionA.Blazor.Interactive package scope and design | open | developer | interactive | 0066 |
| 0071 | Implement Mark #11 superseded — replaced by container and editors split | done | developer | playground-components | 0040 |
| 0072 | Implement Close stale pipeline-generated issues | done | developer | — | 0064 |
| 0073 | Implement Add deprecation markers to Blog.Builder WYSIWYG builders | open | developer | blog | 0068 |
| 0074 | Test Close stale pipeline-generated issues | open | tester | — | 0072 |
| 0075 | Implement Add bUnit tests to Blog display components | done | developer | blog | 0067 |
| 0076 | Test Add bUnit tests to Blog display components | open | tester | blog | 0075 |
| 0077 | Test Batch-close 18 stale/completed issues | inprogress | tester | — | 0062 |
| 0078 | Fix OptionA.Blazor.Interactive build break | open | developer | interactive-package | — |
| 0079 | Test PlaygroundCodeGenerator helper | open | tester | playground-components | 0063 |
| 0080 | Test Mark #19 Done — superseded by scaffold issue #7 | open | tester | playground-core | 0035 |
| 0081 | Test Persist superseded status for issue #16 in authoritative workspace state | open | tester | — | 0041 |
| 0082 | Test Mark #11 superseded — replaced by container and editors split | open | tester | playground-components | 0071 |
