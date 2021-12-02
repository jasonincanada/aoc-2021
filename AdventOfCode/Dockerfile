FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /aoc

# restore the project dependencies
COPY AdventOfCode.sln .
COPY AdventOfCode.Console/AdventOfCode.Console.csproj AdventOfCode.Console/AdventOfCode.Console.csproj
COPY AdventOfCode.CSharp/AdventOfCode.CSharp.csproj AdventOfCode.CSharp/AdventOfCode.CSharp.csproj
COPY AdventOfCode.Tests/AdventOfCode.Tests.csproj AdventOfCode.Tests/AdventOfCode.Tests.csproj
RUN dotnet restore

# copy the rest of the source over
COPY . .
RUN dotnet publish -c release -o /app

# final image
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app

COPY --from=build /app .
COPY Inputs/Day*.txt .

ENTRYPOINT ["dotnet", "AdventOfCode.Console.dll"]
