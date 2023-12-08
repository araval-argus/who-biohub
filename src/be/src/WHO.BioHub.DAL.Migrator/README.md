# WHO.BioHub.DAL.Migrator <!-- omit in TOC -->

## Index <!-- omit in TOC -->

- [Introduction](#introduction)
- [Create new Migrations](#create-new-migrations)
- [Apply generated migrations to SQL Azure](#apply-generated-migrations-to-sql-azure)
  - [Thoubleshooting](#thoubleshooting)
    - [Firewall Rule](#firewall-rule)
    - [Azure Active Directory account can not apply migrations](#azure-active-directory-account-can-not-apply-migrations)

## Introduction

`WHO.BioHub.DAL.Migrator` is a simple Command Line Application that applies WHO.BioHub's database migrations.
The application's entry point ([`Program.cs`](./Program.cs)) performs the following operations:

- Loads configuration from the file `appsettings.json`.
- Loads configuration from Environment Variables.
  - Important is the env var `ConnectionStrings__SQLServer`.
- Configures the logger, leveraging on Serilog.
- Configures Dependency Injection.

Furthermore, the class **BioHubDbContextFactory** is needed by `dotnet-ef` (cfr. section [Create new Migrations](#create-new-migrations)) to inject a constructor for DbContext.

## Create new Migrations

To work with migrations install `dotnet-ef` using the command:

```bash
dotnet tool install --global dotnet-ef --version <VERSION>
```

To add a new migration, you can leverage on the script [`src/be/scripts/add-migration.sh`](src/be/scripts/add-migration.sh).
This script makes extensive use of Docker in order to spin up an instance of SQL Server 2019, configure the application `WHO.BioHub.DAL.Migrator` to use it, and execute `dotnet-ef` to generate a new migration.

_BASH_

```
./add-migration.sh "{NameOfNewMigration}"
```

_Command Promt_

```
add-migration.sh "{NameOfNewMigration}"
```

_Powershell_

```
.\add-migration.sh "{NameOfNewMigration}"
```

Once the services have been registered with success and new migrations have been added to the DAL project get the `IContextMigrator` from the scope and migrate data to the database.

## Apply generated migrations to SQL Azure

**All the generated migrations have to be applied manually** by who is in charge to change the database schema during the entire application lifecycle.
We decided to adopt this approach because applying the migrations when the application starts is not a good practice for a lot of reasons like:

- Azure Function App startup is not predictable and should be light.
- Increases the application startup time.
- Gives the application a responsability that is not in its own scope.
- In case of deployment of a wrong version of the application, it is possible to break or damage database data.

So that's why we decided to use a separate application to perform the migrations in order to have a full control.
Based on that, another important advice is that all the new migrations have to maintain the backward compatibility.
This means that you will apply the migrations and test for backward-compatibility.
Finally, you deploy the new version of the application.

To apply a migration or a new set of migrations, you must follow these steps:

- If not yet installed, install the Azure CLI.
- Run `az login` (authenticate yourself, with personal AAD account)
- Run `az account show` to identify the specific subscription where the database belongs to (grab the "id" field of the specific listed account)
- Run `az account set --subscription <subscription_id>`
- Go to the `src/be/src/WHO.BioHub.DAL.Migrator` folder.
- Run `dotnet run migrator` (the application that will run the migrations)

Before confirming the database update please double check the prompted Connection String.

> To easily switch between Azure Subscriptions you can rely also on the tool [azs](github.com/filariow/azs)

### Thoubleshooting

#### Firewall Rule

If you are unable to sign into the database with the personal AAD account via SSMS (Your client IP address does not have access to the server ...), you must register your IP in the SQL Azure's firewall.

Go to Azure Portal, select the database and then set the server firewall rule by adding your IP address.
Alternatively, you can use the following Azure CLI commands:

```bash
# grant network traffic to the database
az sql server firewall-rule create \
    -n "<RULE_NAME>" \ # it's the name to use for the rule
    -g "<RESOURCE_GROUP>" \
    -s "<SQL_SERVER_NAME>" \
    --end-ip-address "<YOUR_IP>" \
    --start-ip-address "<YOUR_IP>"

# when you are done, remove the firewall rule
az sql server firewall-rule delete \
  -n "<RULE_NAME>" \ # same name as before
  -g "<RESOURCE_GROUP>" \
  -s "<SQL_SERVER_NAME>"
```

> Please always remove your IP address once you have done your duties)

#### Azure Active Directory account can not apply migrations

If your Azure Active Directory (AAD) account is not registered or does not have the grants to perform the migrations, you have to ask to the Admin of the database to enable you.

The administrator will register you on the database and grant you the rights to apply migrations.
Please find some examples below:

```sql
CREATE USER [<username>@who.int] FROM EXTERNAL PROVIDER;
EXEC sp_addrolemember 'db_datawriter', [<username>@who.int];
EXEC sp_addrolemember 'db_datareader', [<username>@who.int];
EXEC sp_addrolemember 'db_owner', [<username>@who.int]; -- if you need to have the grant to apply the migrations
```
