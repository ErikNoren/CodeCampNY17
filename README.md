# Using Entity Framework Core with Relational and JSON Data in SQL Server 2016

## Overview

This project was created for a talk at [Code Camp NYC](http://codecampnyc.org/) that took place on October 14th, 2017.

This demonstrates using Entity Framework Core with SQL Server 2016 to store and work with JSON data. One particular reason for taking advantage of JSON support in SQL Server is to continue storing relational data with database enforced schema and fast querying while also allowing flexible changes to subsets of the data. The JSON data can change schema as enforced by the application itself without requiring larger changes to the database, entities, services, etc. Applications like forms can benefit from this a great deal.

## Project Structure

The project was created using the dotnet angular template: `dotnet new angular`. The templated components remain unchanged aside from the app and navigation changes that are required to add new components demonstrating the various concepts. The specific changes are new Controllers, Models, and ClientApp components. Everything else should be part of the base template.

## Requirements to Run

**As configured, the project will attempt to create a database on (local) called CodeCamp17. If there is no local database installed or the compatability is below 130 / SQL Server 2016 it will still be created but some queries that rely on the new JSON sql commands will fail. You can change this behavior by modifying the `appsettings.json` file to point to another SQL server.**

The dotnet angular template requires node and the .NET 2.0 SDK installed. The new components were created using a mix of Visual Studio 2017 (version 15.3 or higher) for the controllers, models and Entity Framework Core additions, and Visual Studio Code for the new Angular components. I find Code to be an easier development environment for working with the TypeScript sources and HTML templates.

The EF Core models have Migration scripts to create the necessary relational database to run against the samples. Take a look at the *_CreateDatabase.cs file in the Migrations folder for a couple manual additions that add check constraints on the database tables to ensure the columns that store JSON data only accept valid JSON. The connection string can be altered in the appsettings.json file. If you don't have a SQL server you can use to run the samples, try switching to the in-memory provider and remove the migration call in the CodeCampSampleContext.

When the project is started in debug mode, HMR is set up to watch for changes to the ClientApp sources and reload the application without stopping and restarting the project. Only changes to the C# code generally requires stopping the debug session to rebuild and then restart debugging.

Once the project is running you can use PostMan or similar REST client to look at CRUD operators against the Json* Controllers. The sample Angular code currently is create-only (I'm not an Agular developer myself and haven't had someone to collaborate with). The Angular components are more useful in demonstrating this isolation of schema changes. After running the application try making a change to the FormModel, add new form elements and submit. Look at what the services and database receive.

## Resources

[Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

[SQL Server JSON Support](https://docs.microsoft.com/en-us/sql/relational-databases/json/json-data-sql-server) (SQL Server 2016 or higher)

[Mock Data Generator](https://www.mockaroo.com/)

[Angular Forms](https://angular.io/guide/forms)

## Feedback

Feedback is welcome. File issues or pull requests for new features or ping me on Twitter @ErikNoren.
