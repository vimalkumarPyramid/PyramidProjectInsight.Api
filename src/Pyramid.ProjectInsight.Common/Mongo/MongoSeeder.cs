using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Pyramid.ProjectInsight.Common.Mongo
{
    /// <summary>
    /// mongo seeder class
    /// </summary>
    public class MongoSeeder : IDatabaseSeeder
    {
        /// <summary>
        /// mongo db instance
        /// </summary>
        protected readonly IMongoDatabase Database;

        public MongoSeeder(IMongoDatabase database)
        {
            Database = database;
        } 

        /// <summary>
        /// seed mongo db 
        /// </summary>
        /// <returns>return status</returns>
        public async Task SeedAsync()
        {
            var collectionsCursor = await Database.ListCollectionsAsync();
            var collections = await collectionsCursor.ToListAsync();
            if (collections.Any())
            {
                return;
            }
            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}