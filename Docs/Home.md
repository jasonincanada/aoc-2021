# Advent of Code 2021

This is the [Obsidian](https://obsidian.md/) vault for my [Advent of Code 2021](https://adventofcode.com/) solutions.  I'm doing this year's challenge in the Azure cloud to build experience with Microsoft's cloud systems and how they interconnect.  The architecture may not make too much sense, but the more important goal is familiarity with the various components of the cloud and the overall development workflow


## Technologies

| Component      | Implementation                                                                                                |
| -------------- | ------------------------------------------------------------------------------------------------------------- |
| Language       | C# for enviro/plumbing, F# for puzzle logic                                                                   |
| Store          | [Azure SQL Database](https://docs.microsoft.com/en-ca/azure/azure-sql/)                                       |
| User Interface | [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)                                            |
| Computation    | [Azure Functions](https://azure.microsoft.com/en-us/services/functions/)                                      |
| Logging        | [App Insights](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview)                |
| Editor         | [Visual Studio 2022 Community](https://devblogs.microsoft.com/visualstudio/visual-studio-2022-now-available/) |
| Testing        | [Moq](https://github.com/moq/moq4)                                                                            |


## Ideas
- Develop/test locally but use an actual serverless compute resource deployed to the cloud to get the final solution
- Dig into billing components and try to quantify cost in some organized way

## Todo

- [x] Dockerize it
- [ ] Reporting the answer
	- [x] Write to stdout via CLI app
	- [ ] Write to App Insights and inspect with Azure Portal
	- [ ] Write to table
- [x] Solution with C# project
- [ ] Add F# project
- [ ] 6:00am daily task in the cloud to pull my input from the site to some store
