FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV PATH_TO_PROJECT="src/EFK.SampleApp.Ui/EFK.SampleApp.Ui.csproj"

WORKDIR /src
COPY . .

RUN dotnet restore $PATH_TO_PROJECT
RUN dotnet build $PATH_TO_PROJECT --no-restore -c Release
RUN dotnet publish $PATH_TO_PROJECT --no-build -c Release -o /app/publish

FROM nginx:1.25.2-alpine-slim

WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
