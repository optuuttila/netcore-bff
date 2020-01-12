FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

ARG SOURCE_FOLDER=src/netcore-bff

# Copy the src+test
WORKDIR /app
COPY . ./
WORKDIR /app/src
RUN dotnet restore

# build app
WORKDIR /app/$SOURCE_FOLDER
RUN dotnet publish -c Release -o out

# copy tests and run
WORKDIR /app/src
RUN dotnet test

# this will be the final build
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
# GCP AppEngine requires that port 8080 is exposed
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/src/netcore-bff/out ./
ENTRYPOINT ["dotnet", "netcore_bff.dll"]
