FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /src 
COPY *.sln ./
COPY Sample.Api/Sample.Api.csproj Sample.Api/
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Sample.Api.dll"]

