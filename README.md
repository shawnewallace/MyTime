# MyTime


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