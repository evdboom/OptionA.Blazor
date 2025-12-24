# OptionA.Blazor.Server.Test

This is a Blazor Server test project for manual testing of OptionA.Blazor components in a server-side environment.

## Purpose

This project allows testing component behavior in Blazor Server, which can differ from WebAssembly due to:
- Different rendering modes
- Server-side state management
- SignalR communication
- Different service lifetimes (IJSRuntime is scoped in Server vs singleton in WASM)

## Shared Content

This project shares test pages and components with the WASM test project through the `OptionA.Blazor.Test.Shared` Razor Class Library. This ensures both projects test the same components with the same content.

## Known Limitations

Some OptionA components that depend on `IJSRuntime` (like `IResponsiveService` and `IStorageService`) are registered as Singleton in the library but require Scoped lifetime in Blazor Server. Therefore, these services are not registered in this test project to avoid DI validation errors.

Pages that use these services may not function fully in this test project.

## Running the Project

```bash
dotnet run
```

Then navigate to `https://localhost:5001` (or the configured port) in your browser.
