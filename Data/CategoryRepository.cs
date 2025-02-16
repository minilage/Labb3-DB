using MongoDB.Driver;
using TinasLabb03.Model;

namespace TinasLabb03.Data
{
    /// <summary>
    /// Repository för att hantera CRUD-operationer för Category-dokument i MongoDB.
    /// </summary>

    public class CategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(MongoContext context)
        {
            _categories = context.Categories;
        }

        // Hämtar alla Category-dokument asynkront.
        public async Task<List<Category>> GetAllAsync()
        {
            System.Diagnostics.Debug.WriteLine("GetAllSync(Category) anropad");
            return await _categories.Find(_ => true).ToListAsync();
        }

        // Lägger till en ny kategori asynkront.
        public async Task AddAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
        }

        // Uppdaterar en existerande kategori baserat på dess Id.
        public async Task UpdateAsync(Category category)
        {
            await _categories.ReplaceOneAsync(c => c.Id == category.Id, category);
        }

        // Tar bort en kategori baserat på dess Id.
        public async Task DeleteAsync(string id)
        {
            await _categories.DeleteOneAsync(c => c.Id == id);
        }
    }
}