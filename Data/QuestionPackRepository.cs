using MongoDB.Driver;
using TinasLabb03.Model;

namespace TinasLabb03.Data
{
    /// <summary>
    /// Repository för att hantera CRUD-operationer för QuestionPack-dokument i MongoDB.
    /// Använder MongoDB-driver för att kommunicera med databasen via en MongoContext.
    /// </summary>

    public class QuestionPackRepository
    {
        // Privat variabel som refererar till QuestionPack-collectionen i databasen.
        private readonly IMongoCollection<QuestionPack> _collection;


        /// Konstruktor som tar emot en MongoContext och hämtar QuestionPacks-collectionen.
        public QuestionPackRepository(MongoContext context)
        {
            _collection = context.QuestionPacks;
        }

        // Hämtar alla QuestionPack-dokument asynkront.
        public async Task<List<QuestionPack>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        // Lägger till ett nytt QuestionPack-dokument i databasen asynkront.
        public async Task AddAsync(QuestionPack pack)
        {
            await _collection.InsertOneAsync(pack);
        }

        // Uppdaterar ett existerande QuestionPack-dokument baserat på dess Id.
        public async Task UpdateAsync(QuestionPack pack)
        {
            await _collection.ReplaceOneAsync(p => p.Id == pack.Id, pack);
        }

        // Tar bort ett QuestionPack-dokument från databasen baserat på dess Id.
        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(p => p.Id == id);
        }
    }
}