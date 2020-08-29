using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Pyramid.ProjectInsight.Api.Models;

namespace Pyramid.ProjectInsight.Api.Repositories
{
    /// <summary>
    /// class: activity repository for get and create activity
    /// </summary>
    public class ActivityRepository : IActivityRepository
    {
        #region Private Variables
        /// <summary>
        /// interface mongo database
        /// </summary>
        private readonly IMongoDatabase _database;
        #endregion

        #region Constructor
        /// <summary>
        /// It will initialize the mongo database
        /// </summary>
        /// <param name="database"></param>
        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }
        #endregion

        /// <summary>
        /// for get activity by id
        /// </summary>
        /// <param name="id">activity id</param>
        /// <returns>selected activity</returns>
        public async Task<Activity> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// for browse activity
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>browsed activity</returns>
        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
            => await Collection
                .AsQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync();

        /// <summary>
        /// for add activity
        /// </summary>
        /// <param name="activity">activity</param>
        /// <returns>return saved activity</returns>
        public async Task AddAsync(Activity activity)
            => await Collection.InsertOneAsync(activity);

        private IMongoCollection<Activity> Collection 
            => _database.GetCollection<Activity>("Activities");
    }
}