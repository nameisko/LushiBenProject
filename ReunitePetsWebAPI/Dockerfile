#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ReunitePetsWebAPI/ReunitePetsWebAPI.csproj", "ReunitePetsWebAPI/"]
COPY ["ClassLibrary/ClassLibrary.csproj", "ClassLibrary/"]
RUN dotnet restore "ReunitePetsWebAPI/ReunitePetsWebAPI.csproj"
COPY . .
WORKDIR "/src/ReunitePetsWebAPI"
RUN dotnet build "ReunitePetsWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReunitePetsWebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReunitePetsWebAPI.dll"]