namespace BBH.BitBesql;

public class BBHContextSqlite_BitBesql : DbContext
{
    public DbSet<BrowserUserHistoryData> BrowserUserHistoryData { get; set; }
    public BBHContextSqlite_BitBesql(DbContextOptions<BBHContextSqlite_BitBesql> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BrowserUserHistoryData>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Url).IsRequired().HasMaxLength(2048);
            entity.Property(e => e.Date).HasConversion<string>();
        });
    }
}