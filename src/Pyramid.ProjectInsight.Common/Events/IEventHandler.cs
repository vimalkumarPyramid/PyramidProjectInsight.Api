using System.Threading.Tasks;

namespace Pyramid.ProjectInsight.Common.Events
{
    /// <summary>
    /// class for handle subscribe event
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventHandler<in T> where T : IEvent
    {
        /// <summary>
        /// handle subscribe event
        /// </summary>
        /// <param name="event">generic object</param>
        /// <returns></returns>
        Task HandleAsync(T @event);
    }
}