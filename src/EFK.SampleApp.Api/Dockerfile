FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV PATH_TO_PROJECT="src/EFK.SampleApp.Api/EFK.SampleApp.Api.csproj"

WORKDIR /src
COPY . .

RUN dotnet restore $PATH_TO_PROJECT
RUN dotnet build $PATH_TO_PROJECT --no-restore -c Release
RUN dotnet publish $PATH_TO_PROJECT --no-build -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EFK.SampleApp.Api.dll"]
