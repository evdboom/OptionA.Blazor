# Issue 0054: Design EF-inspired IndexedDB API surface

- Status: done
- Role: architect
- Area: storage
- Priority: 25
- Depends On: none
- Roadmap Item: 1
- Family: storage
- External: none
- Pipeline: 42
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Supersedes design phase of #6. Produce an ADR covering: scope decision (full EF Core provider vs standalone EF-inspired surface), proposed DbContext-equivalent API, entity configuration approach, migration strategy (building on existing FileServiceMigrations), query pattern design. Output concrete interfaces and data model contracts. Create follow-up implementation issues after design approval. #6 should be closed once this design issue and its implementation children exist.

## Latest Run

- Run: 28
- Status: Completed
- Model: gpt-5.4
- Session: devteam-architect-50884f7a1606
- Updated: 2026-04-22T14:44:47.2091428+00:00
- Summary: Fit check: this issue fits in one focused architect run as an ADR/decomposition pass; implementation is intentionally split out.

Planning feedback applied: this design keeps `OptionA.Blazor.Storage` as a reusable package surface and does **not** recreate any GitHub Pages work, which remains external to this repo.

**ADR decision:** choose a **standalone EF-inspired IndexedDB surface** over a full EF Core provider. **Approach A rejected:** a real EF Core provider would add large dependency and translation complexity that does not fit the current lightweight package split (`OptionA.Blazor.Storage.Contracts` is `netstandard2.1` with minimal dependencies) or the browser-only IndexedDB/JS-interop runtime. **Approach B rejected:** thin helpers on top of `IDatabaseService` would preserve string-based store/index access and would not deliver the intended typed authoring model. **Recommended:** keep the existing migration executor and JS bridge as the low-level substrate, then add a typed context/set/query/model layer above it with an explicit browser-scoped MVP.

**DbContext-equivalent:** one `IndexedDbContext` instance maps to one IndexedDB database and exposes `Set<TEntity>()`, `InitializeAsync()`, and `SaveChangesAsync()`. `IndexedDbSet<TEntity>` exposes `FindAsync`, `Add`, `Upsert`, `Remove`, and `Query()`. The context uses explicit unit-of-work state only for entities passed to `Add/Upsert/Remove`; v1 does **not** include implicit dirty detection or EF-style full change tracking. Queries return detached entities by default.

**Query design:** no general `IQueryable` provider. Use a constrained async query surface that can map cleanly to IndexedDB: key lookup, declared-index equality/range lookup, paging, `ToListAsync`, `SingleOrDefaultAsync`, `AnyAsync`, and `CountAsync`. Anything outside that set must fail explicitly or require an opt-in client-side materialization step. No joins, grouping, arbitrary LINQ translation, or provider parity with EF Core.

**Entity configuration:** code-first fluent model building via `OnModelCreating(IndexedDbModelBuilder builder)`, with expression-based configuration for keys, indexes, store options, and required properties. Example shape: `builder.Entity<TEntity>("storeName", e => e.HasKey(x => x.Id).HasIndex(x => x.Email).HasDatabaseName("idx_email").IsUnique());`. The model compiles once at startup.

**Migration strategy:** preserve `Migration`, `StoreMigration`, `IndexMigration`, `StoreOptions`, and `MigrationBuilder` as the execution engine. The new typed model layer compiles down to those existing primitives, so `FileServiceMigrations\M001_FileSystem_Initialize` remains valid as proof that the low-level migration path stays intact. For v1, use explicit/manual snapshot-based migrations for typed contexts; do **not** attempt full EF-style provider tooling or automatic migration generation yet. New typed migrations should live in context-specific namespaces/folders rather than `FileServiceMigrations`.

**Concrete contracts:**`IIndexedDbContext { IIndexedDbSet<TEntity> Set<TEntity>(); Task InitializeAsync(CancellationToken); Task<int> SaveChangesAsync(CancellationToken); }``IIndexedDbSet<TEntity> { ValueTask<TEntity?> FindAsync(object key, CancellationToken); void Add(TEntity entity); void Upsert(TEntity entity); void Remove(TEntity entity); IIndexedDbQuery<TEntity> Query(); }``IIndexedDbQuery<TEntity> { WhereEquals(...); WhereBetween(...); Skip(int); Take(int); Task<List<TEntity>> ToListAsync(...); Task<TEntity?> SingleOrDefaultAsync(...); Task<bool> AnyAsync(...); Task<int> CountAsync(...); }``IIndexedDbMigrator` plans/applies typed-context migrations over the existing migration primitives. `IIndexedDbExecutor` abstracts JS/IndexedDB operations and must be constructor-injected. Any timestamp conventions must use `TimeProvider`, and any browser/file access must stay behind explicit abstractions rather than raw API calls.

**Data model contracts:** `DatabaseModel(Name, Version, Stores)`, `EntityStoreModel(ClrType, StoreName, Key, Properties, Indexes, Options)`, `PropertyModel(Name, ClrType, SerializedName, IsRequired)`, `KeyModel(Properties, AutoIncrement)`, `IndexModel(Name, Properties, IsUnique)`, and `PendingChange(EntityType, EntityState, Entity, Key)`. This ADR supersedes the design portion of #6; once the child issues below are persisted, #6 should be closed as replaced by this ADR plus the implementation split.
- Skills Used: brainstorm
- Tools Used: skill(brainstorm), report_intent- multi_tool_use.parallel- glob- rg- view
- Changed Files: none

## Recent Decisions

- #64 [run] Run #28 Completed: Fit check: this issue fits in one focused architect run as an ADR/decomposition pass; implementation is intentionally split out.

Planning feedback applied: this design keeps `OptionA.Blazor.Storage` as a reusable package surface and does **not** recreate any GitHub Pages work, which remains external to this repo.

**ADR decision:** choose a **standalone EF-inspired IndexedDB surface** over a full EF Core provider. **Approach A rejected:** a real EF Core provider would add large dependency and translation complexity that does not fit the current lightweight package split (`OptionA.Blazor.Storage.Contracts` is `netstandard2.1` with minimal dependencies) or the browser-only IndexedDB/JS-interop runtime. **Approach B rejected:** thin helpers on top of `IDatabaseService` would preserve string-based store/index access and would not deliver the intended typed authoring model. **Recommended:** keep the existing migration executor and JS bridge as the low-level substrate, then add a typed context/set/query/model layer above it with an explicit browser-scoped MVP.

**DbContext-equivalent:** one `IndexedDbContext` instance maps to one IndexedDB database and exposes `Set<TEntity>()`, `InitializeAsync()`, and `SaveChangesAsync()`. `IndexedDbSet<TEntity>` exposes `FindAsync`, `Add`, `Upsert`, `Remove`, and `Query()`. The context uses explicit unit-of-work state only for entities passed to `Add/Upsert/Remove`; v1 does **not** include implicit dirty detection or EF-style full change tracking. Queries return detached entities by default.

**Query design:** no general `IQueryable` provider. Use a constrained async query surface that can map cleanly to IndexedDB: key lookup, declared-index equality/range lookup, paging, `ToListAsync`, `SingleOrDefaultAsync`, `AnyAsync`, and `CountAsync`. Anything outside that set must fail explicitly or require an opt-in client-side materialization step. No joins, grouping, arbitrary LINQ translation, or provider parity with EF Core.

**Entity configuration:** code-first fluent model building via `OnModelCreating(IndexedDbModelBuilder builder)`, with expression-based configuration for keys, indexes, store options, and required properties. Example shape: `builder.Entity<TEntity>("storeName", e => e.HasKey(x => x.Id).HasIndex(x => x.Email).HasDatabaseName("idx_email").IsUnique());`. The model compiles once at startup.

**Migration strategy:** preserve `Migration`, `StoreMigration`, `IndexMigration`, `StoreOptions`, and `MigrationBuilder` as the execution engine. The new typed model layer compiles down to those existing primitives, so `FileServiceMigrations\M001_FileSystem_Initialize` remains valid as proof that the low-level migration path stays intact. For v1, use explicit/manual snapshot-based migrations for typed contexts; do **not** attempt full EF-style provider tooling or automatic migration generation yet. New typed migrations should live in context-specific namespaces/folders rather than `FileServiceMigrations`.

**Concrete contracts:**`IIndexedDbContext { IIndexedDbSet<TEntity> Set<TEntity>(); Task InitializeAsync(CancellationToken); Task<int> SaveChangesAsync(CancellationToken); }``IIndexedDbSet<TEntity> { ValueTask<TEntity?> FindAsync(object key, CancellationToken); void Add(TEntity entity); void Upsert(TEntity entity); void Remove(TEntity entity); IIndexedDbQuery<TEntity> Query(); }``IIndexedDbQuery<TEntity> { WhereEquals(...); WhereBetween(...); Skip(int); Take(int); Task<List<TEntity>> ToListAsync(...); Task<TEntity?> SingleOrDefaultAsync(...); Task<bool> AnyAsync(...); Task<int> CountAsync(...); }``IIndexedDbMigrator` plans/applies typed-context migrations over the existing migration primitives. `IIndexedDbExecutor` abstracts JS/IndexedDB operations and must be constructor-injected. Any timestamp conventions must use `TimeProvider`, and any browser/file access must stay behind explicit abstractions rather than raw API calls.

**Data model contracts:** `DatabaseModel(Name, Version, Stores)`, `EntityStoreModel(ClrType, StoreName, Key, Properties, Indexes, Options)`, `PropertyModel(Name, ClrType, SerializedName, IsRequired)`, `KeyModel(Properties, AutoIncrement)`, `IndexModel(Name, Properties, IsUnique)`, and `PendingChange(EntityType, EntityState, Entity, Key)`. This ADR supersedes the design portion of #6; once the child issues below are persisted, #6 should be closed as replaced by this ADR plus the implementation split.