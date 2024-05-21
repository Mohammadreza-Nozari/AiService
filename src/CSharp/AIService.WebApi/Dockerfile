FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY . .
WORKDIR src/CSharp
RUN dotnet restore "AIService.WebApi/AIService.WebApi.csproj"
WORKDIR "/src/CSharp"
RUN dotnet build "AIService.sln" -c Release -o /app/build

WORKDIR "/src/CSharp/AIService.WebApi"
RUN dotnet build "AIService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AIService.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AIService.WebApi.dll"]