using BBH;
using BBH.BitBesql;
using BBH.SqliteWasmBlazor;
using BBH.UI;
using BrowserHistory.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using SqliteWasmBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

await LoadConfigurationAsync(builder);
var dbProviderStr = builder.Configuration["DatabaseProvider"];
switch(dbProviderStr)
{
    case "SqliteWasmBlazor":
        dbProvider= DatabaseProvider.SqliteWasmBlazor;
        break;
    case "BitBesql":
        dbProvider= DatabaseProvider.BitBesql;
        break;
    default:
        throw new Exception($"Unknown DatabaseProvider {dbProviderStr}");
}

switch(dbProvider)
{
    case DatabaseProvider.SqliteWasmBlazor:
        builder.Services.AddDbContextFactory<BBHContextSqlite_SqliteWasmBlazor>(options =>
        {
            var connection = new SqliteWasmConnection("Data Source=HistorySqliteWasmBlazor.db");
            options.UseSqliteWasm(connection);
        });
        builder.Services.AddSingleton<IBrowserUserHistoryRepositoryDatabase, SqliteDatabase_SqliteWasmBlazor>();


        break;
    case DatabaseProvider.BitBesql:

        builder.Services.AddBesqlDbContextFactory<BBHContextSqlite_BitBesql>(optionsAction: options =>
        {
            options.UseSqlite("Data Source=HistoryBitBesql.db");
        }
        );
        builder.Services.AddSingleton<IBrowserUserHistoryRepositoryDatabase, SqliteDatabase_BitBesql>();

        break;
    default:
        throw new Exception($"Unsupported DatabaseProvider {dbProvider}");
}


builder.Services.AddSingleton<IBrowserUserHistoryRepository, BrowserUserHistoryRepository>();

if (dbProvider == DatabaseProvider.SqliteWasmBlazor)
{
    builder.Services.AddSingleton<IDBInitializationService, DBInitializationService>();
}

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

if (dbProvider == DatabaseProvider.SqliteWasmBlazor)
{
    await app.Services.InitializeSqliteWasmDatabaseAsync<BBHContextSqlite_SqliteWasmBlazor>();
}
await using var scope = app.Services.CreateAsyncScope();
switch (dbProvider)
{
    case DatabaseProvider.BitBesql:
        var cntFact_BitBesql = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BBHContextSqlite_BitBesql>>();
        using (var db = await cntFact_BitBesql.CreateDbContextAsync())
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
        break;
    case DatabaseProvider.SqliteWasmBlazor:

        var cntFact_SqliteWasmBlazor = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BBHContextSqlite_SqliteWasmBlazor>>();
        using (var db = await cntFact_SqliteWasmBlazor.CreateDbContextAsync())
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
        break;
}

await app.RunAsync();

public enum DatabaseProvider
{
    None=0,
    SqliteWasmBlazor,
    BitBesql
}
partial class Program
{
    public static DatabaseProvider dbProvider = DatabaseProvider.None;

    public static async Task LoadConfigurationAsync(WebAssemblyHostBuilder builder)
    {
        using var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
        var response = await http.GetAsync("appsettings.json");
        var json = await response.Content.ReadAsStringAsync();
        var config = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            .Build();
        var dict = new Dictionary<string, string?> { };

        foreach (var kvp in config.AsEnumerable())
        {
            dict[kvp.Key] = kvp.Value;
        }
        builder.Configuration.AddInMemoryCollection(dict.ToArray());

    }
}