FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ./src/MyTime.Api/*.csproj src/MyTime.Api/
COPY ["./src/MyTime.App/*.csproj", "src/MyTime.App/"]
COPY ["./src/MyTime.Persistence/*.csproj", "src/MyTime.Persistence/"]

RUN dotnet restore "src/MyTime.Api/MyTime.Api.csproj"

COPY ["./src/MyTime.Api/", "src/MyTime.Api/"]
COPY ["./src/MyTime.App/", "src/MyTime.App/"]
COPY ["./src/MyTime.Persistence/", "src/MyTime.Persistence/"]

WORKDIR /app/src/MyTime.Api
RUN dotnet build -c release --no-restore

FROM build AS publish
RUN dotnet publish "./MyTime.Api.csproj" -c Release -o "./out/" --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime 
RUN useradd -M -s /bin/true -u 1000 dotnet

WORKDIR /app

COPY --from=publish /app/src/MyTime.Api/out ./

# Make the filesystem read-only to the application.
# Directories that the app needs read-write rights should be added explicitly
# Rationale: (1) It discourages people from storing data in ephemeral containers and
# (2) Limits the damage a compromise to the application can precipitate
USER 1000:1000
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT ["dotnet", "MyTime.Api.dll"]
EXPOSE 5000

