# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# This directory is created on the docker image
WORKDIR /src

# Copy the solution to docker image and restore dependencies
COPY MovieWebApp.sln ./
COPY MovieApp.Api/*.csproj ./MovieApp.Api/
COPY MovieApp.Application/*.csproj ./MovieApp.Application/
COPY MovieApp.Domain/*.csproj ./MovieApp.Domain/
COPY MovieApp.Infrastructure/*.csproj ./MovieApp.Infrastructure/
RUN dotnet restore MovieApp.Api/MovieApp.Api.csproj

# Copy all file, e.g source code and build
COPY .. .
WORKDIR /src/MovieApp.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
#Compiles your C# source code into DLLs.
#
#Collects all runtime dependencies (NuGet packages, config, static files) needed to run the app.
#
#Places everything into a single folder (/app/publish in your image).


# This is the creation of the final image, lighter, contains only dotnet runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "MovieApp.Api.dll"]

# docker buildx build --load -t movieapp:latest . (Use this command to run the container)
# docker run -d -p 5000:5000 --name movieapp-container movieapp:latest