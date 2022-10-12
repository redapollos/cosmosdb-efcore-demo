using efcore_cosmosdb.Models;

namespace efcore_cosmosdb.Data
{
    public class SeedDb
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration config)
        {
            var context = serviceProvider.GetRequiredService<CosmosContext>();

            // start with fresh db
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // add a new menu with nested MenuItems
            var m = new Menu()
            {
                Name = "Example Menu",
                Slug = "example-menu",
                Items = new List<MenuItem>()
                {
                    new MenuItem() // this one gets into the cosmosdb
                    {
                        Name = "Menu 1",
                        Slug = "menu-1",
                        Items = new List<MenuItem>()
                        {
                            new MenuItem() // this level doesn't exist in cosmos... Why not? :( 
                            {
                                Name = "Menu 1a",
                                Slug = "menu-1a"
                            }
                        }
                    }
                }
            };

            context.Menus.Add(m);
            await context.SaveChangesAsync();
        }        
    }
}
