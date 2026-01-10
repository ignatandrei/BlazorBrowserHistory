namespace BrowserHistory.Models;

/// <summary>
/// maybe later split in CQRS form ...
/// </summary>
public interface IBrowserUserHistoryRepository
{
    int MaxMemoryDataBeforeSave { get; set; }
    void AddToMemory(BrowserUserHistoryData historyData);
    BrowserUserHistoryData[] DebugData();
    BrowserVisits[] FromMemory();
    void SaveMemory();
    IEnumerable<BrowserVisits> Retrieve(DateTime date);
    IEnumerable<BrowserVisits> MostUsed();
}