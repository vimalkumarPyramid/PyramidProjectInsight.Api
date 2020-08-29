namespace Pyramid.ProjectInsight.Common.Events
{
    /// <summary>
    /// class to handle if user rejected
    /// </summary>
    public class AuthenticateUserRejected : IRejectedEvent
    {
        public string Email { get; }
        public string Code { get; }
        public string Reason { get; }

        /// <summary>
        /// default constructor
        /// </summary>
        protected AuthenticateUserRejected()
        {
        }

        /// <summary>
        /// initialize rejected values
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="code">rejected code</param>
        /// <param name="reason">reason for reject</param>
        public AuthenticateUserRejected(string email,
            string code, string reason)
        {
            Email = email;
            Code = code;
            Reason = reason;
        }         
    }
}