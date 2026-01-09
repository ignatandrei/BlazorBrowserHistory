namespace BrowserHistory.Models;
public record BrowserVisits(string url,string name,int nrVisits);
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

    public string UniqueKey
    {
        get
        {
            return Url + "_" + Date.ToString("yyyyMMddHHmmssfff") + "_" + UserName;
        }
    }
}
