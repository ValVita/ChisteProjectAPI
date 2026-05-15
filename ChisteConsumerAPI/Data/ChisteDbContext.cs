using ChisteConsumerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChisteConsumerAPI.Data
{
    public class ChisteDbContext : DbContext
    {
        public ChisteDbContext(DbContextOptions<ChisteDbContext> options) : base(options) { }
        public DbSet<ChisteModelConsumer> Jokes { get; set; }
    }
}
