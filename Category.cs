using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPIExample.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Relasi one-to-many ke Product
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
