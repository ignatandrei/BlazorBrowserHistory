using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<BBH>("BlazorBrowserHistory");
builder.Build().Run();
