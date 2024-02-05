# Getting Started with .NET Web App

## Prereqs

This project is created using .NET 6.0 and PostgreSQL as the database

Open up appsettings.json and change the ConnectionStrings with your own credential

To migrate the database go to tools ==> NuGet Package Manager ==> Package Manager Console

run the following

## `remove-migration`

to remove previous migration first

## `add-migration initial`

to make a new migration

## `update-database`

to update your database based on the migration

After all done, run the project by pressing ctrl + f5
