
using System.Globalization;

namespace BBH.UI;

public class BBHContextSqlite : DbContext
{
    public DbSet<BrowserUserHistoryData> BrowserUserHistoryData { get; set; }
    public BBHContextSqlite(DbContextOptions<BBHContextSqlite> options) : base(options) { }
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
