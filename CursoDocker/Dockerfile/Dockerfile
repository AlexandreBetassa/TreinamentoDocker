FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
    WORKDIR /src
    COPY *.sln ./
    COPY CursoDocker.csproj ./
    RUN dotnet restore
    COPY . ./
    RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
    WORKDIR /app
    COPY --from=build /out .
    EXPOSE 80
    CMD ["dotnet", "CursoDocker.dll"]