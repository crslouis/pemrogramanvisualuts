using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GrandLineBooks.Models
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty; // service name
        public string Author { get; set; } = string.Empty; // provider/type
        public string ISBN { get; set; } = string.Empty; // SKU
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

