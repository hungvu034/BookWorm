FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base 
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build 
WORKDIR /src
COPY . .
RUN dotnet build "BookWorn.csproj" -c Release -o /app/build
RUN dotnet publish "BookWorn.csproj" -c Realease -o /app/publish

FROM base 
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet" , "BookWorn.dll" ]