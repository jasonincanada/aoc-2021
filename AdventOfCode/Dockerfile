FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /aoc

# restore the project dependencies
COPY AdventOfCode.sln .
COPY AdventOfCode.Console/AdventOfCode.Console.csproj AdventOfCode.Console/AdventOfCode.Console.csproj
RUN dotnet restore

# copy the rest of the source over
COPY . .
RUN dotnet publish -c release -o /app --no-restore

# final image
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app

COPY --from=build /app .
COPY AdventOfCode.Console/Input*.txt .

ENTRYPOINT ["dotnet", "AdventOfCode.Console.dll"]
