FROM mcr.microsoft.com/dotnet/core/sdk:3.1.100-buster AS build
WORKDIR /sln

COPY ./*.sln ./

COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore -r linux-x64 
COPY ./ ./
WORKDIR /sln/Api
RUN dotnet build -r linux-x64 -c Release

FROM build AS test
LABEL test=true
WORKDIR /sln/UnitTests
RUN dotnet tool install trx2junit --version 1.2.6 --tool-path /tools
RUN dotnet build -c Release --no-restore
RUN dotnet test -c Release --no-build --no-restore --results-directory /testresults --logger "trx;LogFileName=results.trx"
RUN /tools/trx2junit --output /testresults/junit /testresults/*.trx

FROM build AS publish
LABEL test=false
WORKDIR /sln/Api
RUN dotnet publish -r linux-x64 -c Release -o /app --no-restore --no-build

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS="http://+:8080"

RUN groupadd --gid 1000 crew \
    && useradd -s /bin/bash --uid 1000 --gid 1000 -m crew
USER crew

COPY --from=publish /app .

ENTRYPOINT ["./Crew.Api.ReferenceImpl.V1"]