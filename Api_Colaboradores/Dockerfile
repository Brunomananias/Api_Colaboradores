#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Api_Colaboradores/Api_Colaboradores.csproj", "Api_Colaboradores/"]
RUN dotnet restore "Api_Colaboradores/Api_Colaboradores.csproj"
COPY . .
WORKDIR "/src/Api_Colaboradores"
RUN dotnet build "Api_Colaboradores.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api_Colaboradores.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api_Colaboradores.dll"]