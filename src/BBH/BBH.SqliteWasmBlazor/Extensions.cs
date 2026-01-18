
namespace BBH.SqliteWasmBlazor;

public static class Extensions
{
    extension(IServiceCollection Services)
    {
        public IServiceCollection SqliteWasmBlazor_AddDependencies(string name = "HistorySqliteWasmBlazor.db")
        {

            Services.AddDbContextFactory<BBHContextSqlite_SqliteWasmBlazor>(options =>
            {
                var connection = new SqliteWasmConnection($"Data Source={name}");
                options.UseSqliteWasm(connection);
            });
            Services.AddSingleton<IBrowserUserHistoryRepositoryDatabase, SqliteDatabase_SqliteWasmBlazor>();
            Services.AddSingleton<IDBInitializationService, DBInitializationService>();

            return Services;
        }
    }
    extension(IServiceProvider ServiceProvider)
    {
        public async Task SqliteWasmBlazor_CreateDatabase()
        {
            await ServiceProvider.InitializeSqliteWasmDatabaseAsync<BBHContextSqlite_SqliteWasmBlazor>();
            await using var scope = ServiceProvider.CreateAsyncScope();
            var cntFact_SqliteWasmBlazor = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BBHContextSqlite_SqliteWasmBlazor>>();
            using (var db = await cntFact_SqliteWasmBlazor.CreateDbContextAsync())
            {
                try
                {
                    //await db.Database.EnsureDeletedAsync();
                    await db.Database.EnsureCreatedAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"DB Creation Error, probably exists {ex.Message}");
                }
            }
        }
    }
}
