Project CRM

Contains entities:
- Accounts: root entity, contains account info
- AccountBalances: sub entitiy for account (1:1 table for linked with Accounts), contains balance info
- AccountOwners: (M:M table for connecting Accounts and Contacts)
- Contacts: root entity, contains contact info
- ContactAddress: sub entity for contact, (1:M table containing contact addreses)
- ContactPhones: sub entity for contact, (1:M table containing contact phones)

Authorization:
- OpenFGA: https://github.com/openfga/dotnet-sdk
- separated service AuthZ: https://github.com/eko/authz

Used stack:
- TargetFramework: net9.0
- LangVersion: 13
- ASP.NET Core 9.0
- GraphQL for http requests: HotChocolate.AspNetCore nuget (https://github.com/ChilliCream/graphql-platform)
- EF Core 9.0: https://github.com/dotnet/efcore
- EF Core migrations 9.0
- MediatR: https://github.com/jbogard/MediatR
- Validation for requests: System.Component.Model
- OpenTelemetry: for tracing
- Prometheus: for metrics
- Serilog: for logs
- All logs, traces, metrics are uploaded to Aspire Dashboard instance

Data base:
- Postgress

Project structure:
- src/Crm.Api contains: web API part, controllers
    - Controllers: contains controllers
    - Infrastructure: contains infrastructure code
- src/Crm.Core contains: business Logic, MediatR requests and handlers
    - Modules contains modules separated by root entities
        - Each module for example "Accounts", contains requests, models, handlers inside
    - Infrastructure: contains infrastructure code
- src/Crm.Data contains: data access layer, entities, their configurations, EF Core migrations
    - Migrations: contains EF Core migrations
    - Entities: contains entities folders separated by root types
        - EntityName (e.g. Accounts)
            - Configurations: folder containing separated file for EF core table configuration by entity
    - Infrastructure: contains infrastructure code
- docker-compose.yaml: contains all needed services for running application localy: Postgress, AuthZ instance, Aspire dashboard instance for logs, metrics and traces
- dockerfile.yaml: for building Crm.Api service docker image


RULES:
- never use preview or prerelease versions of nuget packages
- never add tags TargetFramework, ImplicitUsings, Nullable, LangVersion to the csproj files, it's already added there via Directory.Build.props file
- entities are classes from Crm.Data assembly describing domain models
- for not null fields use the "required" instead of setting default values. Ignore this rule for entities (classes in the Crm.Data assembly describing db model)
- generate constructors for Crm.Data entities and make private setters for the properties, don't use "required" field, but use default values if the field is not nullable
- in c# classes always put constructor after properties, fields constants, but before methods
- use separated file for command, it's handler and it's validator. Store them in the entities folders they belongs to. For example: CreateAccount command would have structure: Accounts/Commands/CreateAccount.cs Accounts/Commands/CreateAccountHandler.cs request and reponse for CreateAccount are in the Accounts/CreateAccount.cs file
- if possible, always use primary constructors for entities. Ignore this rule for entities (classes in the Crm.Data assembly describing db model)
- use Guid.CreateVersion7() if you are generating ids for db entities
- use separated file for db table configuration by entity (IEntityTypeConfiguration<TEntity>)