
namespace BBH.BitBesql;

public static class Extensions
{
    extension(IServiceCollection Services)
    {
        public IServiceCollection BitBesql_AddDependencies(string name = "HistoryBitBesql.db")
        {
            Services.AddBesqlDbContextFactory<BBHContextSqlite_BitBesql>(optionsAction: options =>
            {
                options.UseSqlite($"Data Source={name}");
            }
            );
            Services.AddSingleton<IBrowserUserHistoryRepositoryDatabase, SqliteDatabase_BitBesql>();

            return Services;
        }
    }
    extension(IServiceProvider ServiceProvider)
    {
        public async Task BitBesql_CreateDatabase()
        {
            using var scope = ServiceProvider.CreateScope();
            var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BBHContextSqlite_BitBesql>>();
            using var dbContext = await dbContextFactory.CreateDbContextAsync();
            try
            {
                //await db.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB Creation Error, probably exists {ex.Message}");
            }


        }
    }
}
