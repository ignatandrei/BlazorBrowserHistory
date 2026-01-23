using Blazor.Extension;
using JavaScriptExtensionsAspire;
using Projects;
var builder = DistributedApplication.CreateBuilder(args);

var bbh= builder.AddProject<BBH>("BlazorBrowserHistory")
    .AddCommandsToModifyEnvName(new BBH(), "SqliteWasmBlazor", "BitBesql");

var js = builder
    .AddJavaScriptApp("UITests", "../BBH.UI.Tests")
    .WithReference(bbh)
    //.WithReferenceEnvironment(bbh)
    .AddNpmCommandsFromPackage()
    .ExcludeFromManifest()
    .WithExplicitStart();
    

builder.Build().Run();
