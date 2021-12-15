# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY . .
RUN dotnet restore Hahn Application/Hahn.ApplicatonProcess.Web/Hahn.ApplicatonProcess.Web.csproj \ 
	&& dotnet publish Hahn Application/Hahn.ApplicatonProcess.Web/Hahn.ApplicatonProcess.Web.csproj -c Release -f net6.0 -o /api

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Hahn.ApplicatonProcess.Web.dll"]