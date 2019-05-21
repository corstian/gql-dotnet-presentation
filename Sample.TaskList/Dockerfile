FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Sample.TaskList/Sample.TaskList.csproj", "Sample.TaskList/"]
RUN dotnet restore "Sample.TaskList/Sample.TaskList.csproj"
COPY . .
WORKDIR "/src/Sample.TaskList"
RUN dotnet build "Sample.TaskList.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sample.TaskList.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sample.TaskList.dll"]