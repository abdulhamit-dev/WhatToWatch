WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ./WhatToWatch.Worker/*.csproj ./WhatToWatch.Worker/
COPY ./WhatToWatch.Business/*.csproj ./WhatToWatch.Business/
COPY ./WhatToWatch.Core/*.csproj ./WhatToWatch.Core/
COPY ./WhatToWatch.DataAccess/*.csproj ./WhatToWatch.DataAccess/
COPY ./WhatToWatch.Entities/*.csproj ./WhatToWatch.Entities/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./WhatToWatch.Worker/*.csproj -o /publish/ 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhatToWatch.Worker.dll"]