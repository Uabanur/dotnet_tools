FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy required projects
Copy ./FlackyBackend ./FlackyBackend
Copy ./Common ./Common

# Restore as distinct layers
RUN dotnet restore FlackyBackend

# Build and publish a release
RUN dotnet publish FlackyBackend -c Release -o /App/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "FlackyBackend.dll"]
