FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /aoc

# restore the project dependencies
COPY AdventOfCode.sln .
COPY AdventOfCode.Console/AdventOfCode.Console.csproj AdventOfCode.Console/AdventOfCode.Console.csproj
COPY AdventOfCode.CSharp/AdventOfCode.CSharp.csproj AdventOfCode.CSharp/AdventOfCode.CSharp.csproj

# remove the tests/web projects and restore the other two
RUN dotnet sln remove AdventOfCode.Tests/AdventOfCode.Tests.csproj
RUN dotnet sln remove AdventOfCode.Web/AdventOfCode.Web.csproj
RUN dotnet restore

# copy the source over
COPY AdventOfCode.Console AdventOfCode.Console/
COPY AdventOfCode.CSharp  AdventOfCode.CSharp/

RUN dotnet publish -c release -o /app

# final image
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app

COPY --from=build /app .
COPY Inputs/Day*.txt ./

ENTRYPOINT ["dotnet", "AdventOfCode.Console.dll"]
