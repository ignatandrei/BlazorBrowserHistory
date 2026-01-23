namespace BBH.SqliteWasmBlazor;

public class SqliteDatabase_SqliteWasmBlazor : IBrowserUserHistoryRepositoryDatabase
{
    private readonly IDbContextFactory<BBHContextSqlite_SqliteWasmBlazor> contextFactory;

    public string NameProvider() => "SqliteWasmBlazor";

    public SqliteDatabase_SqliteWasmBlazor(IDbContextFactory<BBHContextSqlite_SqliteWasmBlazor> context)
    {
        this.contextFactory = context;
    }


    public async Task<DateOnly[]> RetrieveLastDates(int nrDates)
    {
        using var cnt = await contextFactory.CreateDbContextAsync();
        var distinctDates = cnt.BrowserUserHistoryData
            .AsAsyncEnumerable()
            .Select(it => new { it.Date.Year, it.Date.Month, it.Date.Day })
            .Select(it => new DateOnly(it.Year, it.Month, it.Day))
            .Distinct()
            .OrderByDescending(it => it)
            .Take(nrDates)

            ;
        var lst = new List<DateOnly>();
        await foreach (var date in distinctDates)
        {
            lst.Add(date);
        }
        return lst.ToArray();
    }
    public async Task<BrowserVisits[]> Retrieve(DateTime date)
    {
        using var cnt = await contextFactory.CreateDbContextAsync();
        var dt = date.Date;
        var dtNext = dt.AddDays(1).AddMinutes(1);
        var allVisits = await cnt.BrowserUserHistoryData
            .Where(it => it.Date >= dt && it.Date < dtNext)
            .Select(it => new BrowserVisits(it.Url, it.PageName, 0))
            .ToArrayAsync();

        return BrowserVisits.Consolidate(allVisits);
    }

    public async Task Save(params BrowserUserHistoryData[] historyData)
    {
        if (historyData.Length == 0) return;
        using var cnt = await contextFactory.CreateDbContextAsync();
        cnt.BrowserUserHistoryData.AddRange(historyData);
        await cnt.SaveChangesAsync();

    }
}

