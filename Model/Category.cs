using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TinasLabb03.Model
{
    // Representerar en kategori för frågepaket.

    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  // Id genereras automatiskt av MongoDB

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty; // Namnet på kategorin

        // Parameterlös konstruktor (nödvändig för deserialisering)
        public Category() { }

        // Konstruktor med namnparameter
        public Category(string name)
        {
            Name = name;
        }
    }
}