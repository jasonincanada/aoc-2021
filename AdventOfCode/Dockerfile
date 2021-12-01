FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /aoc

# restore the project dependencies
COPY AdventOfCode.sln .
COPY AdventOfCode.Console/AdventOfCode.Console.csproj AdventOfCode.Console/AdventOfCode.Console.csproj
COPY AdventOfCode.CSharp/AdventOfCode.CSharp.csproj AdventOfCode.CSharp/AdventOfCode.CSharp.csproj
COPY AdventOfCode.FSharp/AdventOfCode.FSharp.fsproj AdventOfCode.FSharp/AdventOfCode.FSharp.fsproj
RUN dotnet restore

# copy the rest of the source over
COPY . .
RUN dotnet publish -c release -o /app

# final image
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app

COPY --from=build /app .
COPY AdventOfCode.Console/Inputs/Day*.txt .

ENTRYPOINT ["dotnet", "AdventOfCode.Console.dll"]
