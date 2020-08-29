using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Pyramid.ProjectInsight.Common.Mongo
{
    /// <summary>
    /// class for handle mongo db intializatiin
    /// </summary>
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly bool _seed;
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSeeder _seeder;

        /// <summary>
        /// initlize field for mongo db
        /// </summary>
        /// <param name="database">mongo database instance</param>
        /// <param name="seeder">seeder</param>
        /// <param name="options">mongo db options</param>
        public MongoInitializer(IMongoDatabase database, 
            IDatabaseSeeder seeder,
            IOptions<MongoOptions> options)
        {
            _database = database;
            _seeder = seeder;
            _seed = options.Value.Seed;
        }

        /// <summary>
        /// for initialize mongo db
        /// </summary>
        /// <returns>return database seeder</returns>
        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }
            RegisterConventions();
            _initialized = true;
            if (!_seed)
            {
                return;
            }
            await _seeder.SeedAsync();
        }

        /// <summary>
        /// for register conventions
        /// </summary>
        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActioConventions", new MongoConvention(), x => true);
        }

        /// <summary>
        /// class for manage convention
        /// </summary>
        private class MongoConvention : IConventionPack
        {
            /// <summary>
            /// handle convention
            /// </summary>
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}