#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./WhatToWatch.Worker/*.csproj ./WhatToWatch.Worker/
COPY ./WhatToWatch.Business/*.csproj ./WhatToWatch.Business/
COPY ./WhatToWatch.Core/*.csproj ./WhatToWatch.Core/
COPY ./WhatToWatch.DataAccess/*.csproj ./WhatToWatch.DataAccess/
COPY ./WhatToWatch.Entities/*.csproj ./WhatToWatch.Entities/
RUN dotnet restore "WhatToWatch.Worker/WhatToWatch.Worker.csproj"
COPY . .
WORKDIR "/src/WhatToWatch.Worker"
RUN dotnet build "WhatToWatch.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhatToWatch.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhatToWatch.Worker.dll"]