# MyTime

[![.NET](https://github.com/shawnewallace/MyTime/actions/workflows/dotnet.yml/badge.svg)](https://github.com/shawnewallace/MyTime/actions/workflows/dotnet.yml)


## Run CosmosDB Emulator Locally
https://docs.microsoft.com/en-us/azure/cosmos-db/linux-emulator?tabs=ssl-netstd21

https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-dotnet-application

## Use Microsoft SQL Server on Linux for Docker Engine
1. install docker
2. install latest sql distro
```
docker pull mcr.microsoft.com/azure-sql-edge
```
3. start sql server instance
```
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=mytimesql mcr.microsoft.com/azure-sql-edge
```
4. Update connection string in your favorite way to store development secrets:
   * appsettings.Development.json
   * UserSecrets
   * Environment Variables







"MyTimeSqlDbContextConnectionString": "Server=tcp:mytimeproddb.database.windows.net,1433;Initial Catalog=my_time_dev;Persist Security Info=False;User ID=mytimesa;Password=01taLdBsmyYt6u4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"


"MyTimeSqlDbContextConnectionString": "Data Source=localhost;Initial Catalog=mytime_app_dev;User Id=sa;Password=MyPass@word;Encrypt=false"