# Use the official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore the dependencies
COPY . ./
RUN dotnet restore

# Build and publish the app in release mode to the out directory
RUN dotnet publish -c Release -o out

# Use the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "TunApi.dll"]
