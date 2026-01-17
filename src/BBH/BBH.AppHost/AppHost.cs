using JavaScriptExtensionsAspire;
using Projects;
var builder = DistributedApplication.CreateBuilder(args);

var bbh= builder.AddProject<BBH>("BlazorBrowserHistory");

var js = builder
    .AddJavaScriptApp("UITests", "../BBH.UI.Tests")
    .WithReference(bbh)
    //.WithReferenceEnvironment(bbh)
    .AddNpmCommandsFromPackage()
    .ExcludeFromManifest()
    .WithExplicitStart();
    

builder.Build().Run();
