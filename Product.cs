using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPIExample.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")] // Supaya tidak ada warning truncation
        public decimal Price { get; set; }

        // Foreign key ke Category
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [JsonIgnore] // Supaya gak looping saat serialisasi JSON
        public Category? Category { get; set; } // ubah ke nullable
    }
}
