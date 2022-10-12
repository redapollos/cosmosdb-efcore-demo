using efcore_cosmosdb.Models;
using Microsoft.EntityFrameworkCore;

namespace efcore_cosmosdb.Data
{
    public class CosmosContext : DbContext
    {
        public CosmosContext(DbContextOptions<CosmosContext> options) : base(options) { }
        public virtual DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Menu>(b =>
            {
                b.HasKey(o => o.Id);
                b.HasPartitionKey(o => o.Id);
                b.HasDiscriminator<string>(nameof(Menu.Discriminator));
            });
        }  
    }  
}
