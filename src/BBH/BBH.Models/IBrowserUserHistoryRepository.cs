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
   
    Task<BrowserVisits[]> Retrieve(DateTime date);

    Task<DateOnly[]> RetrieveLastDates(int nrDates);

    string NameProvider(); 
}
