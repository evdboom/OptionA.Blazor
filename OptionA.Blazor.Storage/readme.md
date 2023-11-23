# OptionA.Blazor.Storage
Services to access Local storage, session storage and indexed db (though indexed db is still a work in progress

For full documentation, releasenotes and examples, go to [option-a.tech](https://www.option-a.tech/documentation/blazor/storage). To full source can be viewed on [github](https://github.com/evdboom/OptionA.Blazor).

## Getting started
To start using the OptionA.Blazor.Storage include the required depencenies in your service provider. The package uses the default .Net Dependency Injection.

### Service collection
To add the services you can use the extension method `AddStorageService` or `AddDatabaseService`.

### Use package
Inject either the `IStorageService` or `IDatabaseService` in your component and service.

## Latest release notes
### 8.0.0
#### Overall
Update to .NET 8

#### New features
- Update package to .NET 8

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