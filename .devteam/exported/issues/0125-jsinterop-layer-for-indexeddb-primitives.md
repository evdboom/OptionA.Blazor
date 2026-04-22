# Issue 0125: JSInterop layer for IndexedDB primitives

- Status: open
- Role: backend-developer
- Area: storage
- Priority: 70
- Depends On: none
- Roadmap Item: 1
- Family: storage
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Implement small JS module (importable) exposing createDatabase(name, version, migrations), get/put/delete/clear/query, and transaction helpers. Keep API minimal and well-typed for easy mapping from migration runner and DbContext wrapper. Bundle with TypeScript typings for maintainability.

## Latest Run

(none)

## Recent Decisions

(none)