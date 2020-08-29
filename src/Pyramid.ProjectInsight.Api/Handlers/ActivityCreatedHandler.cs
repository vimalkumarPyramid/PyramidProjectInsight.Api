using System;
using System.Threading.Tasks;
using Pyramid.ProjectInsight.Api.Models;
using Pyramid.ProjectInsight.Api.Repositories;
using Pyramid.ProjectInsight.Common.Events;

namespace Pyramid.ProjectInsight.Api.Handlers
{
    /// <summary>
    /// it content the subscribe method for activity create
    /// </summary>
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        #region Private Variables
        /// <summary>
        /// activity repository
        /// </summary>
        private readonly IActivityRepository _repository;
        #endregion

        #region Constructor
        /// <summary>
        /// it will initialze the activity repository
        /// </summary>
        /// <param name="repository"></param>
        public ActivityCreatedHandler(IActivityRepository repository)
        {
            _repository = repository;
        }
        #endregion

        /// <summary>
        /// subscribe while activity create
        /// </summary>
        /// <param name="event">activtiy</param>
        /// <returns>http status</returns>
        public async Task HandleAsync(ActivityCreated @event)
        {
            await _repository.AddAsync(new Activity
            {
                Id = @event.Id,
                UserId = @event.UserId,
                Category = @event.Category,
                Name = @event.Name,
                CreatedAt = @event.CreatedAt,
                Description = @event.Description
            });
            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}