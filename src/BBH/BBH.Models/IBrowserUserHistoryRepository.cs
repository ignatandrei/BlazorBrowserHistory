using static BrowserHistory.Models.IBrowserUserHistoryRepository;

namespace BrowserHistory.Models;

/// <summary>
/// maybe later split in CQRS form ...
/// </summary> 
public interface IBrowserUserHistoryRepository : IBrowserUserHistoryRepositoryDatabase
{
    int MaxMemoryDataBeforeSave { get; set; }
    Task AddToMemory(BrowserUserHistoryData historyData);
    BrowserUserHistoryData[] DebugData();
    BrowserVisits[] FromMemory();
    Task SaveMemory();
}

public interface IBrowserUserHistoryRepositoryDatabase
{
    Task Save(params BrowserUserHistoryData[] historyData);
    IEnumerable<BrowserVisits> MostUsed();
    Task<BrowserVisits[]> Retrieve(DateTime date);

}
