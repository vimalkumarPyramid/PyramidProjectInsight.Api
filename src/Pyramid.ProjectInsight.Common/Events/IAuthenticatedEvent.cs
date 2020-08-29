using System;

namespace Pyramid.ProjectInsight.Common.Events
{
    /// <summary>
    /// interface authenticate user
    /// </summary>
    public interface IAuthenticatedEvent : IEvent
    {
        /// <summary>
        /// get user id
        /// </summary>
        Guid UserId { get; }
    }
}