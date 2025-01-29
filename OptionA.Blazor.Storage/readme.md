# OptionA.Blazor.Storage
Services to access Local storage, session storage and indexed db (though indexed db is still a work in progress

For full documentation, releasenotes and examples, go to [option-a.tech](https://www.option-a.tech/documentation/blazor/storage). To full source can be viewed on [github](https://github.com/evdboom/OptionA.Blazor).

## Getting started
To start using the OptionA.Blazor.Storage include the required depencenies in your service provider. The package uses the default .Net Dependency Injection.

### Service collection
To add the services you can use the extension method `AddStorageService`, `AddDatabaseService` or `AddStorageServices`.

### Use package
Inject either the `IStorageService`, `IDatabaseService` or `IFileSystem` in your component and service.

## Latest release notes
### 9.1.0
#### Overall
Added Filesystem sypport

#### New features
- Added IFilesystem implementation to interact with the file system

#### Solved Bugs
- None

## Services
Following are the suppored services.

### Storage service
```
IStorageService
```
Service for accessing local and session storage

### Database service (WIP)
```
IDatabaseService
```
Service for accessing the local indexed database. Uses database migration to keep the indexed db up to date.

### Filesystem service
```
IFilesystem
```
Service for accessing the file system. Uses the javascript Filesystem API to interact with the file system. Most interaction can only be done in the user initiated event (like pressing a button).