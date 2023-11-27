using Guide.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Guide.Infra.Context
{
    public class GuideContext : DbContext
    {
        public GuideContext(DbContextOptions<GuideContext> options) : base(options)
        {
        }
        public DbSet<PriceRecord> PriceRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
