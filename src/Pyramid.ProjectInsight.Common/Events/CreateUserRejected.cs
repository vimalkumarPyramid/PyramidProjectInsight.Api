namespace Pyramid.ProjectInsight.Common.Events
{
    /// <summary>
    /// class for handle create user rejected
    /// </summary>
    public class CreateUserRejected : IRejectedEvent
    {
        public string Email { get; }
        public string Reason { get; }
        public string Code { get; }

        /// <summary>
        /// default constructor
        /// </summary>
        protected CreateUserRejected()
        {
        }

        /// <summary>
        /// initialize the value for create user rejected
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="reason">reason of reject</param>
        /// <param name="code">rejected code</param>
        public CreateUserRejected(string email, 
            string reason, string code)
        {
            Email = email;
            Reason = reason;
            Code = code;
        }
    }
}