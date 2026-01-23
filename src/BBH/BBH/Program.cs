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
        builder.Services.SqliteWasmBlazor_AddDependencies("HistorySqliteWasmBlazor.db");

        break;
    case DatabaseProvider.BitBesql:

        builder.Services.BitBesql_AddDependencies("HistoryBitBesql.db");
        break;
    default:
        throw new Exception($"Unsupported DatabaseProvider {dbProvider}");
}

builder.Services.AddSingleton<IBrowserUserHistoryRepository, BrowserUserHistoryRepository>();


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

switch (dbProvider)
{
    case DatabaseProvider.BitBesql:
        await app.Services.BitBesql_CreateDatabase();
        break;
    case DatabaseProvider.SqliteWasmBlazor:
        await app.Services.SqliteWasmBlazor_CreateDatabase();
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
    private static async Task< Dictionary<string, string?>> FromSettings(Uri url, string nameSetting)
    {
        string nameFile="";
        try
        {
            if (!string.IsNullOrEmpty(nameSetting))
            {
                nameSetting = "." + nameSetting + ".";
            }
            else
            {
                nameSetting = ".";
            }
            nameFile = $"appsettings{nameSetting}json";
            using var http = new HttpClient { BaseAddress = url };
            var response = await http.GetAsync(nameFile);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var config = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
                .Build();
            var dict = new Dictionary<string, string?> { };

            foreach (var kvp in config.AsEnumerable())
            {
                dict[kvp.Key] = kvp.Value;
            }
            return dict;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"loading {nameFile} send exception {ex.Message}");
            return [];
        }
    }
    public static async Task LoadConfigurationAsync(WebAssemblyHostBuilder builder)
    {
        var BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        var dict = await FromSettings(BaseAddress,"");
        var env = builder.HostEnvironment.Environment;
        var res = await FromSettings(BaseAddress, env);
        foreach(var kvp in res)
        {
            dict[kvp.Key]= kvp.Value;
        }
        builder.Configuration.AddInMemoryCollection(dict.ToArray());

    }
}