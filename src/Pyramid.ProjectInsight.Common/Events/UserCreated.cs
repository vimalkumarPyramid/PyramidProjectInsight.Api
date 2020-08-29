namespace Pyramid.ProjectInsight.Common.Events
{
    /// <summary>
    /// class for handle user created event
    /// </summary>
    public class UserCreated : IEvent
    {
        /// <summary>
        /// user email
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// user name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// default constructor
        /// </summary>
        protected UserCreated()
        {
        }

        /// <summary>
        /// initialize the user email and name
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="name">user name</param>
        public UserCreated(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}