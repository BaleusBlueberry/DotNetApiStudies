using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication3.Models
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
