namespace BrowserHistory.Models;
public record BrowserVisits(string url,string name,int nrVisits)
{
    public static BrowserVisits[] Consolidate(BrowserVisits[] allVisits)
    {
        if (allVisits.Length == 0) return [];
        return allVisits.GroupBy(it => it)
            .Select(g => new BrowserVisits(g.Key.url, g.Key.name, g.Count()))
            .OrderByDescending(it => it.nrVisits)
            .ThenBy(it => it.name)
            .ToArray();
    }
}
public class BrowserUserHistoryData
{
    


    #region database saving stuff

    /// <summary>
    /// to be used just for database related stuff
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// to be used just for database related stuff
    /// </summary>
    public bool IsNew
    {
        get
        {
            return Id == 0;
        }
    }
    #endregion
    public string Url { get; set; } =string.Empty;
    public DateTime Date { get; set; }
    public string UserName { get; set; }=string.Empty;
    public string PageName { get; set; } = string.Empty;
    public string? AdditionalInfo { get; set; } 
    public string UniqueKey
    {
        get
        {
            return Url + "_" + Date.ToString("yyyyMMddHHmmssfff") + "_" + UserName;
        }
    }
}
