using System;

namespace Pyramid.ProjectInsight.Common.Events
{
    /// <summary>
    /// class for handle activity
    /// </summary>
    public class ActivityCreated : IAuthenticatedEvent
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }

        /// <summary>
        /// default construcor
        /// </summary>
        protected ActivityCreated()
        {
        }

        /// <summary>
        /// initialize activity values
        /// </summary>
        /// <param name="id">activity id</param>
        /// <param name="userId">user id</param>
        /// <param name="category">category</param>
        /// <param name="name">name</param>
        /// <param name="description">description</param>
        /// <param name="createdAt">created date</param>
        public ActivityCreated(Guid id, Guid userId,
            string category, string name, 
            string description, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}