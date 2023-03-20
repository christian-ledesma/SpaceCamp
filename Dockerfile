FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app/

# copy .csproj and restore as distinct layers
COPY "SpaceCamp.sln" "SpaceCamp.sln"
COPY "SpaceCamp.API/SpaceCamp.API.csproj" "SpaceCamp.API/SpaceCamp.API.csproj"
COPY "SpaceCamp.Application/SpaceCamp.Application.csproj" "SpaceCamp.Application/SpaceCamp.Application.csproj"
COPY "SpaceCamp.Domain/SpaceCamp.Domain.csproj" "SpaceCamp.Domain/SpaceCamp.Domain.csproj"
COPY "SpaceCamp.Infrastructure/SpaceCamp.Infrastructure.csproj" "SpaceCamp.Infrastructure/SpaceCamp.Infrastructure.csproj"
COPY "SpaceCamp.Persistence/SpaceCamp.Persistence.csproj" "SpaceCamp.Persistence/SpaceCamp.Persistence.csproj"

RUN dotnet restore "SpaceCamp.sln"

# copy everything else build
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out

# build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "SpaceCamp.API.dll" ]