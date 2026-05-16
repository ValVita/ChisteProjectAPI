using ChisteConsumerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChisteConsumerAPI.Data
{
    public class ChisteDbContext : DbContext
    {
        public ChisteDbContext(DbContextOptions<ChisteDbContext> options) : base(options) { }
        public DbSet<ChisteModelConsumer> CHISTEDB { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ... código generado automáticamente
            base.OnModelCreating(modelBuilder);
        }
    }
}
