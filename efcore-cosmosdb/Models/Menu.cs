using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace efcore_cosmosdb.Models
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }

    public class Menu : BaseModel
    {
        public string Discriminator = nameof(Menu);
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Slug { get; set; }

        public ICollection<MenuItem> Items { get; set; } = new List<MenuItem>();
    }

    // I've tried with and with the BaseModel
    public class MenuItem : BaseModel
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Slug { get; set; }
        [MaxLength(1000)]
        public string Url { get; set; }        
        public ICollection<MenuItem> Items { get; set; } = new List<MenuItem>();
    }
}
