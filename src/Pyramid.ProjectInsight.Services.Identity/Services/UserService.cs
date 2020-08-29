using Pyramid.ProjectInsight.Common.Auth;
using Pyramid.ProjectInsight.Common.Exceptions;
using Pyramid.ProjectInsight.Services.Identity.Domain.Models;
using Pyramid.ProjectInsight.Services.Identity.Domain.Repositories;
using Pyramid.ProjectInsight.Services.Identity.Domain.Services;
using System;
using System.Threading.Tasks;


namespace Pyramid.ProjectInsight.Services.Identity.Services
{
    /// <summary>
    /// class for handle user service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        /// <summary>
        /// intialize user repository, encrypter and jwt handler
        /// </summary>
        /// <param name="repository">repository</param>
        /// <param name="encrypter">encrypter</param>
        /// <param name="jwtHandler">jwthandler</param>
        public UserService(IUserRepository repository,
            IEncrypter encrypter,
            IJwtHandler jwtHandler)
        {
            _repository = repository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        /// <summary>
        /// register user
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">password</param>
        /// <param name="name">user name</param>
        /// <returns></returns>
        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _repository.GetAsync(email);
            if (user != null)
            {
                throw new ProjectInsightException("email_in_use",
                    $"Email: '{email}' is already in use.");
            }
            user = new User(email, name);
            user.SetPassword(password, _encrypter);
            await _repository.AddAsync(user);
        }

        /// <summary>
        /// login user
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">password</param>
        /// <returns>return jwttoken</returns>
        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _repository.GetAsync(email);
            if (user == null)
            {
                throw new ProjectInsightException("invalid_credentials",
                    $"Invalid credentials.");
            }
            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new ProjectInsightException("invalid_credentials",
                    $"Invalid credentials.");
            }

            return _jwtHandler.Create(user.Id);
        }
    }
}