using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication4.Models
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation((BsonType.ObjectId))]
        public string? Id { get; set; }

        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public int Quantity { get; set; } = default!;

    }
}
