# BlazorBrowserHistory
Blazor Browser History aims to save the history of the user links into a nice interface .

See https://ignatandrei.github.io/BlazorBrowserHistory/

# How to use

## Install the following package

1. BBH.Models

2. One of the following
    BBH.SqliteWasmBlazor ( use SqliteWasmBlazor for storing in WASM )
    BBH.BitBesql ( use BeSqBit.Besql for storing in WASM  )

## Add to the Blazor WASM project

### For BBH.SqliteWasmBlazor

```code
   var builder = WebAssemblyHostBuilder.CreateDefault(args);
   //code
   builder.Services.SqliteWasmBlazor_AddDependencies("HistorySqliteWasmBlazor.db");
   builder.Services.AddSingleton<IBrowserUserHistoryRepository, BrowserUserHistoryRepository>();
   //code
   var app = builder.Build();
   await app.Services.SqliteWasmBlazor_CreateDatabase();
```

### For BBH.BitBesql
```code
   var builder = WebAssemblyHostBuilder.CreateDefault(args);
   //code
   builder.Services.BitBesql_AddDependencies("HistoryBitBesql.db");
   builder.Services.AddSingleton<IBrowserUserHistoryRepository, BrowserUserHistoryRepository>();
   //code
   var app = builder.Build();
   await app.Services.BitBesql_CreateDatabase();

```

Do not forget to add in index.html 
```code
<script src="_content/Bit.Besql/bit-besql.js"></script>
```

## See the interface

In app.razor modify this
```code
<Router AppAssembly="@typeof(App).Assembly" NotFoundPage="typeof(Pages.NotFound)" AdditionalAssemblies="new[] { typeof(BBH.UI.History).Assembly}"  >

```
## See the interface 

Navigate to /history


## If you want to implement a GUI interface

See https://github.com/ignatandrei/BlazorBrowserHistory/tree/main/src/BBH/BBH.UI project . History.razor contains everything.



# If you want to contribute

## Run the project 

```code
cd src
cd BBH
cd BBH
dotnet watch run --no-hot-reload
```

## Publish and run on local

```code
cd src
cd BBH
dotnet tool restore
dotnet r publishRun
```