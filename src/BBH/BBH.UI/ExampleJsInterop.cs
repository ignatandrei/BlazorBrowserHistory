using Microsoft.JSInterop;
using System.Runtime.InteropServices.JavaScript;

namespace BBH.UI;

public partial class MyInterop 
{
    [JSImport("showPrompt", "InteropData")]
    public static partial string Test(string selector);

    [JSImport("documentTitle", "InteropData")]
    public static partial string GetTitle();
}
