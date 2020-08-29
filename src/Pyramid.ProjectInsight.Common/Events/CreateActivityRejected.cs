using System;

namespace Pyramid.ProjectInsight.Common.Events
{
    /// <summary>
    /// class for handle user rejected
    /// </summary>
    public class CreateActivityRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        /// <summary>
        /// default constructor
        /// </summary>
        protected CreateActivityRejected()
        {
        }

        /// <summary>
        /// intialize the value for activity rejected
        /// </summary>
        /// <param name="id">activity id</param>
        /// <param name="reason">reason of reject</param>
        /// <param name="code">rejected code</param>
        public CreateActivityRejected(Guid id, 
            string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}