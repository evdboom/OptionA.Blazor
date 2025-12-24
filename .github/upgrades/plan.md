# .NET 10 Upgrade Plan

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Risk Management](#risk-management)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Overview

Upgrade the OptionA.Blazor solution from .NET 9.0 to .NET 10.0 (Long Term Support).

### Scope

| Metric | Value |
|--------|-------|
| Total Projects | 10 |
| Projects to Upgrade | 9 (1 remains on netstandard2.1) |
| NuGet Packages to Update | 7 |
| Total Lines of Code | 16,726 |
| Estimated LOC Impact | 78+ (~0.5% of codebase) |
| Security Vulnerabilities | 0 |

### Selected Strategy

**All-At-Once Strategy** - All projects upgraded simultaneously in a single atomic operation.

**Rationale**:
- 10 projects (small/medium solution)
- All currently on .NET 9.0 (homogeneous)
- All projects rated ?? Low difficulty
- Clear dependency structure with no cycles
- All packages have known .NET 10 compatible versions
- Good test coverage (2 unit test projects)

### Complexity Classification

**Simple** - Fast batch execution approach:
- Homogeneous codebase (all .NET 9.0)
- No high-risk projects
- No security vulnerabilities
- Clear upgrade path for all packages

### Critical Issues

None identified. All packages have compatible .NET 10 versions available.

---

## Migration Strategy

### Approach Selection

**All-At-Once Strategy** selected for this upgrade.

All projects will be upgraded simultaneously in a single coordinated operation. This approach is optimal because:
- The solution is small (10 projects)
- All projects are on the same framework version (.NET 9.0)
- No complex inter-dependencies requiring staged migration
- All required packages have .NET 10 compatible versions

### Atomic Upgrade Operations

The following operations will be performed as a single coordinated batch:

1. **Update all project files** - Change TargetFramework from `net9.0` to `net10.0`
2. **Update all package references** - Upgrade 7 packages to .NET 10 versions
3. **Restore dependencies** - Run `dotnet restore`
4. **Build solution** - Compile and fix any compilation errors
5. **Verify** - Solution builds with 0 errors

### Execution Timeline

| Phase | Description | Deliverable |
|-------|-------------|-------------|
| Phase 0 | Preparation | Verify SDK, branch setup |
| Phase 1 | Atomic Upgrade | All projects updated, solution builds |
| Phase 2 | Test Validation | All tests pass |

---

## Detailed Dependency Analysis

### Dependency Graph Summary

The solution has a clear hierarchical structure with no circular dependencies:

```
Level 0 (Leaf Projects - No Dependencies):
??? OptionA.Blazor.Blog.csproj
??? OptionA.Blazor.Components.Direct.csproj
??? OptionA.Blazor.Storage.Contracts.csproj (netstandard2.1 - unchanged)

Level 1 (Intermediate):
??? OptionA.Blazor.Components.csproj ? Components.Direct
??? OptionA.Blazor.Storage.csproj ? Storage.Contracts
??? OptionA.Blazor.Blog.Builder.csproj ? Blog, Components.Direct

Level 2 (Applications & Tests):
??? OptionA.Blazor.Test.csproj ? Components, Storage, Blog.Builder
??? OptionA.Blazor.Maui.Test.csproj ? Components, Blog.Builder
??? OptionA.Blazor.Blog.UnitTests.csproj ? Blog
??? OptionA.Blazor.Components.UnitTests.csproj ? Components
```

### Project Groupings

Since we're using All-At-Once strategy, all projects are upgraded together. For reference, the logical groupings are:

**Library Projects (6)**:
- OptionA.Blazor.Blog
- OptionA.Blazor.Blog.Builder
- OptionA.Blazor.Components
- OptionA.Blazor.Components.Direct
- OptionA.Blazor.Storage
- OptionA.Blazor.Storage.Contracts (stays on netstandard2.1)

**Application Projects (2)**:
- OptionA.Blazor.Test (Blazor WebAssembly)
- OptionA.Blazor.Maui.Test (.NET MAUI Blazor Hybrid)

**Test Projects (2)**:
- OptionA.Blazor.Blog.UnitTests
- OptionA.Blazor.Components.UnitTests

### Critical Path

No critical path concerns - all projects can be upgraded atomically.

---

## Project-by-Project Plans

### OptionA.Blazor.Storage.Contracts

**Current State**:
- Target Framework: `netstandard2.1` ? (unchanged)
- Dependencies: 0 project references
- Dependants: 1 (OptionA.Blazor.Storage)
- LOC: 569
- Risk Level: Low

**Target State**:
- Target Framework: `netstandard2.1` (remains unchanged for cross-platform compatibility)
- Package updates only

**Migration Steps**:
1. Update `Microsoft.Extensions.DependencyInjection.Abstractions` from 9.0.1 ? 10.0.1
2. Update `System.Text.Json` from 9.0.1 ? 10.0.1

**Validation**: Project builds without errors

---

### OptionA.Blazor.Blog

**Current State**:
- Target Framework: `net9.0`
- Dependencies: 0 project references
- Dependants: 2 (Blog.Builder, Blog.UnitTests)
- LOC: 3,859
- Risk Level: Low
- API Issues: 1 source incompatible (`String.Split`)

**Target State**:
- Target Framework: `net10.0`

**Migration Steps**:
1. Update `TargetFramework` from `net9.0` to `net10.0`
2. Update `Microsoft.AspNetCore.Components.Web` from 9.0.1 ? 10.0.1

**Expected Breaking Changes**:
- `String.Split(ReadOnlySpan<char>)` - source incompatible, recompilation should resolve

**Validation**: Project builds without errors

---

### OptionA.Blazor.Components.Direct

**Current State**:
- Target Framework: `net9.0`
- Dependencies: 0 project references
- Dependants: 2 (Components, Blog.Builder)
- LOC: 1,581
- Risk Level: Low

**Target State**:
- Target Framework: `net10.0`

**Migration Steps**:
1. Update `TargetFramework` from `net9.0` to `net10.0`
2. Update `Microsoft.AspNetCore.Components.Web` from 9.0.1 ? 10.0.1

**Validation**: Project builds without errors

---

### OptionA.Blazor.Components

**Current State**:
- Target Framework: `net9.0`
- Dependencies: 1 (Components.Direct)
- Dependants: 3 (Test, Maui.Test, Components.UnitTests)
- LOC: 5,726
- Risk Level: Low

**Target State**:
- Target Framework: `net10.0`

**Migration Steps**:
1. Update `TargetFramework` from `net9.0` to `net10.0`
2. Update `Microsoft.AspNetCore.Components.Web` from 9.0.1 ? 10.0.1

**Validation**: Project builds without errors

---

### OptionA.Blazor.Storage

**Current State**:
- Target Framework: `net9.0`
- Dependencies: 1 (Storage.Contracts)
- Dependants: 1 (Test)
- LOC: 711
- Risk Level: Low

**Target State**:
- Target Framework: `net10.0`

**Migration Steps**:
1. Update `TargetFramework` from `net9.0` to `net10.0`
2. Update `Microsoft.AspNetCore.Components.Web` from 9.0.1 ? 10.0.1
3. Update `Microsoft.Extensions.Configuration.Binder` from 9.0.1 ? 10.0.1

**Validation**: Project builds without errors

---

### OptionA.Blazor.Blog.Builder

**Current State**:
- Target Framework: `net9.0`
- Dependencies: 2 (Blog, Components.Direct)
- Dependants: 2 (Test, Maui.Test)
- LOC: 2,839
- Risk Level: Low

**Target State**:
- Target Framework: `net10.0`

**Migration Steps**:
1. Update `TargetFramework` from `net9.0` to `net10.0`
2. Update `Microsoft.AspNetCore.Components.Web` from 9.0.1 ? 10.0.1

**Validation**: Project builds without errors

---

### OptionA.Blazor.Test (Blazor WebAssembly)

**Current State**:
- Target Framework: `net9.0`
- Project Type: Blazor WebAssembly (AspNetCore)
- Dependencies: 3 (Components, Storage, Blog.Builder)
- Dependants: 0
- LOC: 662
- Risk Level: Low
- API Issues: 3 behavioral changes (`System.Uri`, `Environment.SetEnvironmentVariable`)

**Target State**:
- Target Framework: `net10.0`

**Migration Steps**:
1. Update `TargetFramework` from `net9.0` to `net10.0`
2. Update `Microsoft.AspNetCore.Components.WebAssembly` from 9.0.1 ? 10.0.1
3. Update `Microsoft.AspNetCore.Components.WebAssembly.DevServer` from 9.0.1 ? 10.0.1

**Expected Breaking Changes**:
- `System.Uri` behavioral changes - verify URI parsing at runtime
- `Environment.SetEnvironmentVariable` behavioral change - verify if used

**Validation**: Project builds without errors, manual runtime verification recommended

---

### OptionA.Blazor.Maui.Test (.NET MAUI Blazor Hybrid)

**Current State**:
- Target Framework: `net9.0-windows10.0.19041.0`
- Project Type: .NET MAUI Blazor Hybrid
- Dependencies: 2 (Components, Blog.Builder)
- Dependants: 0
- LOC: 651
- Risk Level: Low
- API Issues: 74 source incompatible, 3 binary incompatible, 3 behavioral changes

**Target State**:
- Target Framework: `net10.0-windows10.0.19041.0`

**Migration Steps**:
1. Update `TargetFrameworks` condition from `net9.0-windows10.0.19041.0` to `net10.0-windows10.0.19041.0`
2. Update `Microsoft.Extensions.Logging.Debug` from 9.0.1 ? 10.0.1
3. `Microsoft.Maui.Controls` (9.0.30) and `Microsoft.AspNetCore.Components.WebView.Maui` (9.0.30) are compatible - no update needed

**Expected Breaking Changes**:
- MAUI API source incompatibilities (BindingMode, MauiApp, etc.) - resolved by recompilation
- `MauiWinUIApplication` binary incompatibility - recompilation required
- Behavioral changes in `System.Uri` - verify at runtime

**Note**: The 74+ API issues are primarily source incompatibilities that will be resolved during recompilation. No code changes expected.

**Validation**: Project builds without errors, manual runtime verification recommended

---

### OptionA.Blazor.Blog.UnitTests

**Current State**:
- Target Framework: `net9.0`
- Project Type: Unit Test (xUnit)
- Dependencies: 1 (Blog)
- LOC: 52
- Risk Level: Low

**Target State**:
- Target Framework: `net10.0`

**Migration Steps**:
1. Update `TargetFramework` from `net9.0` to `net10.0`
2. All test packages (bunit, xunit, Moq, etc.) are already compatible

**Validation**: Project builds, all tests pass

---

### OptionA.Blazor.Components.UnitTests

**Current State**:
- Target Framework: `net9.0`
- Project Type: Unit Test (xUnit)
- Dependencies: 1 (Components)
- LOC: 76
- Risk Level: Low

**Target State**:
- Target Framework: `net10.0`

**Migration Steps**:
1. Update `TargetFramework` from `net9.0` to `net10.0`
2. All test packages (bunit, xunit, Moq, etc.) are already compatible

**Validation**: Project builds, all tests pass

---

## Package Update Reference

### Packages Requiring Updates (7)

| Package | Current | Target | Projects Affected | Update Reason |
|---------|---------|--------|-------------------|---------------|
| Microsoft.AspNetCore.Components.Web | 9.0.1 | 10.0.1 | 5 projects | Framework alignment |
| Microsoft.AspNetCore.Components.WebAssembly | 9.0.1 | 10.0.1 | 1 project | Framework alignment |
| Microsoft.AspNetCore.Components.WebAssembly.DevServer | 9.0.1 | 10.0.1 | 1 project | Framework alignment |
| Microsoft.Extensions.Configuration.Binder | 9.0.1 | 10.0.1 | 1 project | Framework alignment |
| Microsoft.Extensions.DependencyInjection.Abstractions | 9.0.1 | 10.0.1 | 1 project | Framework alignment |
| Microsoft.Extensions.Logging.Debug | 9.0.1 | 10.0.1 | 1 project | Framework alignment |
| System.Text.Json | 9.0.1 | 10.0.1 | 1 project | Framework alignment |

### Compatible Packages (No Update Needed) (8)

| Package | Version | Status |
|---------|---------|--------|
| bunit | 1.38.5 | ? Compatible |
| coverlet.collector | 6.0.4 | ? Compatible |
| Microsoft.AspNetCore.Components.WebView.Maui | 9.0.30 | ? Compatible |
| Microsoft.Maui.Controls | 9.0.30 | ? Compatible |
| Microsoft.NET.Test.Sdk | 17.12.0 | ? Compatible |
| Moq | 4.20.72 | ? Compatible |
| xunit | 2.9.3 | ? Compatible |
| xunit.runner.visualstudio | 3.0.1 | ? Compatible |

### Package Updates by Project

**OptionA.Blazor.Storage.Contracts** (netstandard2.1):
- Microsoft.Extensions.DependencyInjection.Abstractions: 9.0.1 ? 10.0.1
- System.Text.Json: 9.0.1 ? 10.0.1

**OptionA.Blazor.Blog, Components, Components.Direct, Blog.Builder**:
- Microsoft.AspNetCore.Components.Web: 9.0.1 ? 10.0.1

**OptionA.Blazor.Storage**:
- Microsoft.AspNetCore.Components.Web: 9.0.1 ? 10.0.1
- Microsoft.Extensions.Configuration.Binder: 9.0.1 ? 10.0.1

**OptionA.Blazor.Test**:
- Microsoft.AspNetCore.Components.WebAssembly: 9.0.1 ? 10.0.1
- Microsoft.AspNetCore.Components.WebAssembly.DevServer: 9.0.1 ? 10.0.1

**OptionA.Blazor.Maui.Test**:
- Microsoft.Extensions.Logging.Debug: 9.0.1 ? 10.0.1

**OptionA.Blazor.Blog.UnitTests, Components.UnitTests**:
- No package updates required

---

## Breaking Changes Catalog

### Overview

| Category | Count | Impact |
|----------|-------|--------|
| ?? Binary Incompatible | 3 | Recompilation required |
| ?? Source Incompatible | 69 | Recompilation resolves |
| ?? Behavioral Change | 6 | Runtime verification needed |

### Binary Incompatibilities (High Impact)

**Affected Project**: OptionA.Blazor.Maui.Test

| API | Description | Resolution |
|-----|-------------|------------|
| `MauiWinUIApplication` constructor | Binary signature changed | Recompile - automatic resolution |

### Source Incompatibilities (Medium Impact)

**Affected Project**: OptionA.Blazor.Maui.Test (68 issues)

Most source incompatibilities are in MAUI framework APIs:
- `BindingMode` enum (20 occurrences)
- `MauiApp`, `MauiAppBuilder` (13 occurrences)
- Various MAUI control APIs

**Resolution**: These are resolved automatically during recompilation. No code changes required.

**Affected Project**: OptionA.Blazor.Blog (1 issue)
- `String.Split(ReadOnlySpan<char>)` - resolved by recompilation

### Behavioral Changes (Low Impact)

**Affected Projects**: OptionA.Blazor.Test, OptionA.Blazor.Maui.Test

| API | Change | Projects | Recommendation |
|-----|--------|----------|----------------|
| `System.Uri` constructor | URI parsing behavior changes | Test, Maui.Test | Verify URI handling at runtime |
| `Environment.SetEnvironmentVariable` | Behavior change | Test | Verify if used in application |

**Note**: These behavioral changes require runtime testing but typically don't cause issues for standard use cases.

---

## Risk Management

### Risk Assessment Summary

| Project | Risk Level | Risk Factors | Mitigation |
|---------|------------|--------------|------------|
| OptionA.Blazor.Storage.Contracts | ?? Low | Package updates only | Standard testing |
| OptionA.Blazor.Blog | ?? Low | 1 source incompatibility | Recompilation |
| OptionA.Blazor.Components.Direct | ?? Low | None | Standard testing |
| OptionA.Blazor.Components | ?? Low | None | Standard testing |
| OptionA.Blazor.Storage | ?? Low | None | Standard testing |
| OptionA.Blazor.Blog.Builder | ?? Low | None | Standard testing |
| OptionA.Blazor.Test | ?? Low | Behavioral changes | Runtime verification |
| OptionA.Blazor.Maui.Test | ?? Low | MAUI API changes | Recompilation + runtime test |
| OptionA.Blazor.Blog.UnitTests | ?? Low | None | Run tests |
| OptionA.Blazor.Components.UnitTests | ?? Low | None | Run tests |

### Key Risks and Mitigations

**Risk 1: MAUI API Source Incompatibilities**
- **Impact**: Medium (74 issues in Maui.Test)
- **Likelihood**: Low (resolved by recompilation)
- **Mitigation**: All issues are source-level; recompilation resolves them automatically

**Risk 2: Behavioral Changes in System.Uri**
- **Impact**: Low
- **Likelihood**: Low
- **Mitigation**: Runtime testing of URI-dependent features

### Contingency Plans

**If build fails after upgrade**:
1. Review error messages for specific API changes
2. Check .NET 10 breaking changes documentation
3. Apply targeted fixes based on error messages

**If tests fail after upgrade**:
1. Identify failing tests
2. Determine if failure is due to behavioral changes
3. Update test expectations or code as needed

### Security Considerations

? No security vulnerabilities identified in current packages.

---

## Testing & Validation Strategy

### Phase-by-Phase Testing

**Phase 1: Build Verification**
- [ ] Solution restores without errors
- [ ] Solution builds without errors
- [ ] No new warnings introduced

**Phase 2: Unit Test Execution**
- [ ] OptionA.Blazor.Blog.UnitTests - All tests pass
- [ ] OptionA.Blazor.Components.UnitTests - All tests pass

**Phase 3: Runtime Verification (Recommended)**
- [ ] OptionA.Blazor.Test - Application starts and runs
- [ ] OptionA.Blazor.Maui.Test - Application starts and runs

### Test Projects

| Test Project | Tests | Framework |
|--------------|-------|-----------|
| OptionA.Blazor.Blog.UnitTests | xUnit + bUnit | Blazor component tests |
| OptionA.Blazor.Components.UnitTests | xUnit + bUnit + Moq | Blazor component tests |

### Validation Checklist

For each project:
- [ ] Builds without errors
- [ ] No new warnings
- [ ] Existing functionality preserved

For test projects:
- [ ] All unit tests pass
- [ ] No test regressions

---

## Complexity & Effort Assessment

### Per-Project Complexity

| Project | Complexity | Dependencies | Package Updates | API Issues |
|---------|------------|--------------|-----------------|------------|
| OptionA.Blazor.Storage.Contracts | Low | 0 | 2 | 0 |
| OptionA.Blazor.Blog | Low | 0 | 1 | 1 |
| OptionA.Blazor.Components.Direct | Low | 0 | 1 | 0 |
| OptionA.Blazor.Components | Low | 1 | 1 | 0 |
| OptionA.Blazor.Storage | Low | 1 | 2 | 0 |
| OptionA.Blazor.Blog.Builder | Low | 2 | 1 | 0 |
| OptionA.Blazor.Test | Low | 3 | 2 | 3 |
| OptionA.Blazor.Maui.Test | Low | 2 | 1 | 74 |
| OptionA.Blazor.Blog.UnitTests | Low | 1 | 0 | 0 |
| OptionA.Blazor.Components.UnitTests | Low | 1 | 0 | 0 |

### Overall Assessment

**Solution Complexity**: Low
- All projects rated Low complexity
- Clear dependency structure
- No blocking issues
- All packages have upgrade paths

---

## Source Control Strategy

### Branching Strategy

- **Source Branch**: `main`
- **Upgrade Branch**: `upgrade-to-NET10` (current)
- **Merge Strategy**: Single PR after all changes complete

### Commit Strategy

**Single Commit Approach** (recommended for All-At-Once):
- Commit all upgrade changes together
- Message: `Upgrade solution to .NET 10.0`
- Include: All project file changes, package updates

### Review Process

1. Complete all upgrades
2. Verify build succeeds
3. Verify all tests pass
4. Create PR from `upgrade-to-NET10` to `main`
5. Review changes
6. Merge

---

## Success Criteria

### Technical Criteria

- [ ] All 9 projects target .NET 10.0 (Storage.Contracts remains netstandard2.1)
- [ ] All 7 package updates applied
- [ ] Solution builds with 0 errors
- [ ] Solution builds with 0 new warnings
- [ ] All unit tests pass (5 tests total)
- [ ] No security vulnerabilities

### Quality Criteria

- [ ] Code quality maintained
- [ ] Test coverage unchanged
- [ ] No functionality regressions

### Process Criteria

- [ ] All-At-Once strategy followed
- [ ] Single atomic commit for upgrade
- [ ] Changes on designated upgrade branch

### Definition of Done

The .NET 10 upgrade is complete when:
1. ? All projects successfully target their designated frameworks
2. ? All package references updated to .NET 10 compatible versions
3. ? Solution builds without errors
4. ? All unit tests pass
5. ? Changes committed to upgrade branch
6. ? (Optional) Runtime verification of application projects
