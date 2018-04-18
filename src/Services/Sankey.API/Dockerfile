FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /src/Sankey.API

# Copy csproj and restore as distinct layers
#COPY ./Sankey.API/*.csproj .
#RUN dotnet restore

# Copy everything else and build
WORKDIR /src
COPY . .
WORKDIR /src/Sankey.API
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /src/Sankey.API/out .
COPY --from=build-env /src/Sankey.API/Setup ./Setup
ENTRYPOINT ["dotnet", "Sankey.API.dll"]