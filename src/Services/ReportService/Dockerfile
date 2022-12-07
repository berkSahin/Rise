#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/ReportService/ReportService.csproj", "src/Services/ReportService/"]
RUN dotnet restore "src/Services/ReportService/ReportService.csproj"
COPY . .
WORKDIR "/src/src/Services/ReportService"
RUN dotnet build "ReportService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.dll"]