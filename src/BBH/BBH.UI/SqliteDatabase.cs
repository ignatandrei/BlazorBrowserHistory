namespace BBH.UI;

public class SqliteDatabase: IBrowserUserHistoryRepositoryDatabase
{
    private readonly IDbContextFactory<BBHContextSqlite> contextFactory;

    public SqliteDatabase(IDbContextFactory<BBHContextSqlite> context)
    {
        this.contextFactory = context;
    }

    public IEnumerable<BrowserVisits> MostUsed()
    {
        throw new NotImplementedException();
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
