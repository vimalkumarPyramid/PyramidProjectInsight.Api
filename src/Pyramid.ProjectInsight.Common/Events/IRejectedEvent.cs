namespace Pyramid.ProjectInsight.Common.Events
{
    /// <summary>
    /// class for handle rejected event
    /// </summary>
    public interface IRejectedEvent : IEvent
    {
        /// <summary>
        /// reason for rejection
        /// </summary>
        string Reason { get; }

        /// <summary>
        /// rejection code
        /// </summary>
        string Code { get; }
    }
}