namespace BBH.UI;
public class BrowserUserHistoryRepository : IBrowserUserHistoryRepository
{
    const int DefaultMaxMemoryDataBeforeSave = 1;
    public BrowserUserHistoryRepository(IBrowserUserHistoryRepositoryDatabase databaseOps)
    {
        this.MaxMemoryDataBeforeSave= DefaultMaxMemoryDataBeforeSave;
        this.databaseOps = databaseOps;
    }
    private List<BrowserUserHistoryData> memory = [];

    public int MaxMemoryDataBeforeSave { 
        get ; 
        set {
            if (value <= 0) value = DefaultMaxMemoryDataBeforeSave;
            field = value;
        } 
    }
    private readonly static Lock _memoryLock = new Lock();
    private readonly IBrowserUserHistoryRepositoryDatabase  databaseOps;

    public async Task AddToMemory(BrowserUserHistoryData historyData)
    {
        memory.Add(historyData);
        if (memory.Count >= MaxMemoryDataBeforeSave)
        {
            //lock (_memoryLock)
            {
                await SaveMemory();
                memory = [];
            }
        }
    }
    public async Task<DateOnly[]> RetrieveLastDates(int nrDates)
    {
        return await this.databaseOps.RetrieveLastDates(nrDates);
    }




    public async Task SaveMemory()
    {
        //lock (_memoryLock)
        {
            await Save(memory.ToArray());
        }
    }

    public BrowserVisits[] FromMemory()
    {
        lock (_memoryLock)
        {
            var allVisits = memory
            .Select(it => new BrowserVisits(it.Url, it.PageName, 0)).ToArray();
            return BrowserVisits.Consolidate(allVisits);
        }
    }
    
    

    


    public async Task<BrowserVisits[]> Retrieve(DateTime date)
    {
        
        return await this.databaseOps.Retrieve(date);
    }

    public BrowserUserHistoryData[] DebugData()
    {
        lock(_memoryLock)
        {
            return memory.ToArray();
        }
    }

    public async Task Save(params BrowserUserHistoryData[] historyData)
    {
        await this.databaseOps.Save(historyData);
    }
}
