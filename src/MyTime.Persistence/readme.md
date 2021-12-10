## To Add a new migration...
When ready to add a new migration, use the api project as the startup project.

`dotnet ef migrations add <name-of-your-migration> --startup-project ../MyTime.Csl/MyTime.Csl.csproj` 

The API project should apply migrations automatically. If you need to apply them manually, then use the following command.

`dotnet ef database update --startup-project ../wei.middleware.api/wei.middleware.api.csproj`