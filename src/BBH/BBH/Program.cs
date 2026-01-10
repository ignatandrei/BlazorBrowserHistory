using BBH;
using BBH.UI;
using BrowserHistory.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using SqliteWasmBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<IBrowserUserHistoryRepository, BrowserUserHistoryRepository>();
builder.Services.AddDbContextFactory<BBHContextSqlite>(options =>
{
    var connection = new SqliteWasmConnection("Data Source=History.db");
    options.UseSqliteWasm(connection);
});
builder.Services.AddSingleton<IDBInitializationService, DBInitializationService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

await app.Services.InitializeSqliteWasmDatabaseAsync<BBHContextSqlite>();

var scope = app.Services.CreateAsyncScope();
var cntFact = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BBHContextSqlite>>();
using (var db = await cntFact.CreateDbContextAsync())
{
    try
    {
        //await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DB Creation Error, probably exists {ex.Message}");
    }
}

await app.RunAsync();

