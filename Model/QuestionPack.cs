using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.ObjectModel;

namespace TinasLabb03.Model
{
    // Representerar ett frågepaket som innehåller flera frågor, en kategori, svårighetsgrad och en tidsgräns.

    public class QuestionPack
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // Låt MongoDB generera ID:t

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("difficulty")]
        public Difficulty Difficulty { get; set; }

        [BsonElement("timeLimitInSeconds")]
        public int TimeLimitInSeconds { get; set; }

        // Kategori sparas som en sträng (namnet), t.ex. "General" eller något användaren väljer
        [BsonElement("category")]
        public string Category { get; set; } = "General";

        [BsonElement("questions")]
        public ObservableCollection<Question> Questions { get; set; } = new();

        // Parameterlös konstruktor med defaultvärden
        public QuestionPack()
        {
            Name = "Default Name";
        }

        // Konstruktor med parametrar för att skapa ett nytt pack
        public QuestionPack(string name, Difficulty difficulty, int timeLimitInSeconds, string category)
        {
            Name = name;
            Difficulty = difficulty;
            TimeLimitInSeconds = timeLimitInSeconds;
            Category = category;
        }
    }
}