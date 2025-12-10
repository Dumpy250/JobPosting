# 1) Runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# 2) Build image (SDK) for restore/build/publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the csproj first (better caching)
COPY ["JobPosting.csproj", "./"]

# Restore dependencies
RUN dotnet restore "JobPosting.csproj"

# Copy the rest of the source (now without bin/obj, thanks to .dockerignore)
COPY . .

# Build and publish
RUN dotnet publish "JobPosting.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3) Final runtime image
FROM base AS final
WORKDIR /app

# Copy from build stage
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "JobPosting.dll"]
