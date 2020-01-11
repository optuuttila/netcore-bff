FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

ARG SOURCE_FOLDER=src/netcore-bff
ARG TEST_FOLDER=test/netcore-bff.test

WORKDIR /app

# copy csproj and restore as distinct layers
COPY src/*.sln ./src/
COPY $SOURCE_FOLDER/*.csproj ./$SOURCE_FOLDER/
COPY $TEST_FOLDER/*.csproj ./$TEST_FOLDER/
WORKDIR /app/src
RUN dotnet restore

# copy everything else and build app
WORKDIR /app
COPY $SOURCE_FOLDER/. ./$SOURCE_FOLDER/
WORKDIR /app/$SOURCE_FOLDER
RUN dotnet publish -c Release -o out

# copy tests and run
WORKDIR /app
COPY $TEST_FOLDER/. ./$TEST_FOLDER/
WORKDIR /app/src
RUN dotnet test

# this will be the final build
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
# GCP AppEngine requires that port 8080 is exposed
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/src/netcore-bff/out ./
ENTRYPOINT ["dotnet", "netcore-bff.dll"]
