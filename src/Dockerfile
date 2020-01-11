FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Cyberkruz.PublishProfile.Web/*.csproj ./Cyberkruz.PublishProfile.Web/
COPY Cyberkruz.PublishProfile.Web.Tests/*.csproj ./Cyberkruz.PublishProfile.Web.Tests/
RUN dotnet restore

# copy everything else and build app
COPY Cyberkruz.PublishProfile.Web/. ./Cyberkruz.PublishProfile.Web/
COPY Cyberkruz.PublishProfile.Web.Tests/. ./Cyberkruz.PublishProfile.Web.Tests/
WORKDIR /app/Cyberkruz.PublishProfile.Web
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/Cyberkruz.PublishProfile.Web/out ./
ENTRYPOINT ["dotnet", "Cyberkruz.PublishProfile.Web.dll"]