# Issue 0056: Implement typed IndexedDB contracts and model builder

- Status: open
- Role: developer
- Area: storage
- Priority: 30
- Depends On: 0054
- Roadmap Item: 1
- Family: storage
- External: none
- Pipeline: 44
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Add the public EF-inspired contracts in OptionA.Blazor.Storage.Contracts: IIndexedDbContext, IIndexedDbSet<TEntity>, IIndexedDbQuery<TEntity>, context options, and model metadata records, plus DI registration such as AddOptionAIndexedDbContext<TContext>(). Keep non-trivial collaborators constructor-injected, introduce an explicit IIndexedDbExecutor abstraction instead of calling JS directly from the public surface, and use TimeProvider for any timestamp conventions.

## Latest Run

(none)

## Recent Decisions

(none)