# Issue 0058: Implement IndexedDbContext CRUD and constrained query pipeline

- Status: open
- Role: developer
- Area: storage
- Priority: 28
- Depends On: 0054
- Roadmap Item: 1
- Family: storage
- External: none
- Pipeline: 46
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Build IndexedDbContext, IndexedDbSet<TEntity>, and the unit-of-work path for Add, Upsert, Remove, FindAsync, SaveChangesAsync, and query execution. Support only key lookups, declared-index equality/range predicates, paging, and async materialization. Do not implement IQueryable, joins, groupings, or implicit dirty detection

## Latest Run

(none)

## Recent Decisions

(none)