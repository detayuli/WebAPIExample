using System.Text.Json.Serialization;

namespace WebAPIExample
{
    public class Product
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }


    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
