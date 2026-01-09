using Microsoft.JSInterop;
using System.Runtime.InteropServices.JavaScript;

namespace BBH.UI;
// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public partial class MyInterop 
{
    [JSImport("showPrompt", "InteropData")]
    public static partial string Test(string selector);

    [JSImport("documentTitle", "InteropData")]
    public static partial string GetTitle();
}
public partial class JsInterop(IJSRuntime jsRuntime) : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BBH.UI/exampleJsInterop.js").AsTask());

    
    public async ValueTask<string> Prompt(string message)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("showPrompt", message);
    }
    public async ValueTask<string> GetTitle()
    {
        var module = await moduleTask.Value;
        return await jsRuntime.InvokeAsync<string>("MyDocumentTitle","test");
    } 
    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
