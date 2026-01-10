namespace BBH.UI;

public class BrowserUserHistoryRepository : IBrowserUserHistoryRepository
{
    const int DefaultMaxMemoryDataBeforeSave = 1;
    public BrowserUserHistoryRepository(IDbContextFactory<BBHContextSqlite> context)
    {
        this.MaxMemoryDataBeforeSave= DefaultMaxMemoryDataBeforeSave;
        this.contextFactory = context;
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
    private readonly IDbContextFactory<BBHContextSqlite> contextFactory;

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
            return Consolidate(allVisits);
        }
    }
    
    private BrowserVisits[] Consolidate(BrowserVisits[] allVisits)
    {
        if(allVisits.Length == 0)    return [];
        return allVisits.GroupBy(it => it)
            .Select(g => new BrowserVisits(g.Key.url, g.Key.name, g.Count()))
            .OrderByDescending(it => it.nrVisits)
            .ThenBy(it => it.name)
            .ToArray();
    }

    public IEnumerable<BrowserVisits> MostUsed()
    {
        throw new NotImplementedException();
    }


    public async Task<BrowserVisits[]> Retrieve(DateTime date)
    {
        using var cnt = await contextFactory.CreateDbContextAsync();
        var dt = date.Date;
        var dtNext = dt.AddDays(1).AddMilliseconds(1);
        var allVisits = await cnt.BrowserUserHistoryData
            .Where(it => it.Date >= date.Date && it.Date<dtNext)
            .Select(it => new BrowserVisits(it.Url, it.PageName, 0))
            .ToArrayAsync();
        
        return Consolidate(allVisits);
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
        if(historyData.Length == 0)  return;
        using var cnt = await contextFactory.CreateDbContextAsync();
        cnt.BrowserUserHistoryData.AddRange(historyData);
        await cnt.SaveChangesAsync();
    }
}
