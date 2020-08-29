using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pyramid.ProjectInsight.Common.Mongo;
using Pyramid.ProjectInsight.Services.Activities.Domain.Models;
using Pyramid.ProjectInsight.Services.Activities.Domain.Repositories;

namespace Pyramid.ProjectInsight.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;

        public CustomMongoSeeder(IMongoDatabase database, 
            ICategoryRepository categoryRepository) 
            : base(database)
        {
            _categoryRepository = categoryRepository;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>
            {
                "work",
                "sport",
                "hobby"
            };
            await Task.WhenAll(categories.Select(x => _categoryRepository
                        .AddAsync(new Category(x))));
        }
    }
}