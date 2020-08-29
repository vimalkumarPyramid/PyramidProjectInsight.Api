using Pyramid.ProjectInsight.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pyramid.ProjectInsight.Api.Repositories
{
    public interface IActivityRepository
    {
        /// <summary>
        /// for get activity by id
        /// </summary>
        /// <param name="id">activity id</param>
        /// <returns>selected activity</returns>
        Task<Activity> GetAsync(Guid id);

        /// <summary>
        /// for browse activity
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>browsed activity</returns>
        Task<IEnumerable<Activity>> BrowseAsync(Guid userId);

        /// <summary>
        /// for add activity
        /// </summary>
        /// <param name="activity">activity</param>
        /// <returns>return saved activity</returns>
        Task AddAsync(Activity activity);
    }
}