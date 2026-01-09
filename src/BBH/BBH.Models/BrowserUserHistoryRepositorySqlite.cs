using BrowserHistory.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrowserHistory.Models;

public class BrowserUserHistoryRepositorySqlite : IBrowserUserHistoryRepository
{
    public BrowserUserHistoryRepositorySqlite()
    {
        this.MaxMemoryDataBeforeSave= 100;
    }
    private List<BrowserUserHistoryData> memory = [];

    public int MaxMemoryDataBeforeSave { 
        get ; 
        set {
            if (value <= 0) value = 100;
            field = value;
        } 
    }
    private readonly static Lock _memoryLock = new Lock();
    public void AddToMemory(BrowserUserHistoryData historyData)
    {
        memory.Add(historyData);
        if (memory.Count > MaxMemoryDataBeforeSave)
        {
            lock (_memoryLock)
            {
                SaveMemory();
                memory = [];
            }
        }
    }

    
    
    
    public void SaveMemory()
    {
        throw new NotImplementedException();
    }

    public BrowserVisits[] FromMemory()
    {
        lock (_memoryLock) { 
            var allVisits = memory
            .Select(it => new BrowserVisits(it.Url, it.PageName, 0)).ToArray();
        return allVisits.GroupBy(it => it)
            .Select(g => new BrowserVisits(g.Key.url, g.Key.name, g.Count()))
            .OrderByDescending(it => it.nrVisits)
            .ThenBy(it => it.name)
            .ToArray();
    }
    }


    public IEnumerable<BrowserVisits> MostUsed()
    {
        throw new NotImplementedException();
    }


    public IEnumerable<BrowserVisits> Retrieve(DateTime date)
    {
        throw new NotImplementedException();
    }
}
