namespace Pyramid.ProjectInsight.Common.Events
{
    /// <summary>
    /// class for handle user authentication
    /// </summary>
    public class UserAuthenticated : IEvent
    {
        /// <summary>
        /// get user email
        /// </summary>
        public string Email { get; }
        
        /// <summary>
        /// default constructor
        /// </summary>
        protected UserAuthenticated()
        {
        }

        /// <summary>
        /// constructor for initialize user email
        /// </summary>
        /// <param name="email">user email</param>
        public UserAuthenticated(string email)
        {
            Email = email;
        }
    }
}