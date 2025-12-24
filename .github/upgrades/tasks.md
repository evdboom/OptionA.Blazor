# OptionA.Blazor .NET 10.0 Upgrade Tasks

## Overview

This document tracks the execution of the OptionA.Blazor solution upgrade from .NET 9.0 to .NET 10.0. All 9 projects (except Storage.Contracts which remains on netstandard2.1) will be upgraded simultaneously in a single atomic operation, followed by comprehensive testing.

**Progress**: 1/3 tasks complete (33%) ![0%](https://progress-bar.xyz/33)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2025-12-24 21:42)*
**References**: Plan §Executive Summary, Assessment

- [✓] (1) Verify .NET 10.0 SDK is installed
- [✓] (2) SDK version meets minimum requirements (**Verify**)

---

### [▶] TASK-002: Atomic framework and package upgrade with compilation fixes
**References**: Plan §Project-by-Project Plans, Plan §Package Update Reference, Plan §Breaking Changes Catalog

- [▶] (1) Update TargetFramework from `net9.0` to `net10.0` in all 9 projects per Plan §Project-by-Project Plans (Blog, Components.Direct, Components, Storage, Blog.Builder, Test, Maui.Test, Blog.UnitTests, Components.UnitTests)
- [ ] (2) Update TargetFrameworks condition from `net9.0-windows10.0.19041.0` to `net10.0-windows10.0.19041.0` in Maui.Test project
- [ ] (3) All project files updated to target frameworks (**Verify**)
- [ ] (4) Update package references per Plan §Package Update Reference (7 packages: Microsoft.AspNetCore.Components.Web 9.0.1→10.0.1 in 5 projects, Microsoft.AspNetCore.Components.WebAssembly 9.0.1→10.0.1, Microsoft.AspNetCore.Components.WebAssembly.DevServer 9.0.1→10.0.1, Microsoft.Extensions.Configuration.Binder 9.0.1→10.0.1, Microsoft.Extensions.DependencyInjection.Abstractions 9.0.1→10.0.1, Microsoft.Extensions.Logging.Debug 9.0.1→10.0.1, System.Text.Json 9.0.1→10.0.1)
- [ ] (5) All package references updated (**Verify**)
- [ ] (6) Run `dotnet restore` for entire solution
- [ ] (7) All dependencies restored successfully (**Verify**)
- [ ] (8) Build solution and fix all compilation errors per Plan §Breaking Changes Catalog (focus: String.Split source incompatibility in Blog project, MAUI API source incompatibilities in Maui.Test project resolved by recompilation)
- [ ] (9) Solution builds with 0 errors (**Verify**)
- [ ] (10) Commit changes with message: "TASK-002: Upgrade OptionA.Blazor solution to .NET 10.0"

---

### [ ] TASK-003: Run full test suite and validate upgrade
**References**: Plan §Testing & Validation Strategy

- [ ] (1) Run tests in OptionA.Blazor.Blog.UnitTests project
- [ ] (2) Run tests in OptionA.Blazor.Components.UnitTests project
- [ ] (3) Fix any test failures (reference Plan §Breaking Changes Catalog for behavioral changes in System.Uri and Environment.SetEnvironmentVariable)
- [ ] (4) Re-run tests after fixes
- [ ] (5) All tests pass with 0 failures (**Verify**)
- [ ] (6) Commit test fixes with message: "TASK-003: Complete .NET 10.0 upgrade testing and validation"

---


