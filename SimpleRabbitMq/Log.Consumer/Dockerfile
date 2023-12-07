FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy required projects
Copy ./Log.Consumer ./Log.Consumer
Copy ./Common ./Common

# Restore as distinct layers
RUN dotnet restore Log.Consumer

# Build and publish a release
RUN dotnet publish Log.Consumer -c Release -o /App/out


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Log.Consumer.dll"]