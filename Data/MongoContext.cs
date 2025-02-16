using MongoDB.Driver;
using TinasLabb03.Model;

namespace TinasLabb03.Data
{
    // Hanterar anslutningen till MongoDB och exponerar collections.

    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        // Konstruktor med hårdkodade värden
        public MongoContext()
        {
            // Hårdkodad anslutningssträng och databasnamn
            var connectionString = "mongodb://localhost:27017";
            var databaseName = "TinaLagesson";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Exponerar QuestionPacks-collectionen.
        public IMongoCollection<QuestionPack> QuestionPacks =>
            _database.GetCollection<QuestionPack>("QuestionPacks");

        // Exponerar Categories-collectionen.
        public IMongoCollection<Category> Categories =>
            _database.GetCollection<Category>("Categories");
    }
}