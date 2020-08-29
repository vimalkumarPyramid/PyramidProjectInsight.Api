using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pyramid.ProjectInsight.Common.Commands;
using Pyramid.ProjectInsight.Common.Events;
using Pyramid.ProjectInsight.Common.Exceptions;
using Pyramid.ProjectInsight.Services.Identity.Services;
using RawRabbit;

namespace Pyramid.ProjectInsight.Services.Identity.Handlers
{
    /// <summary>
    /// class for hanle user create
    /// </summary>
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;

        /// <summary>
        /// constructor for initialize busclient,userservice and log information
        /// </summary>
        /// <param name="busClient">bus client</param>
        /// <param name="userService">user service</param>
        /// <param name="logger">log information</param>
        public CreateUserHandler(IBusClient busClient,
            IUserService userService, 
            ILogger<CreateUser> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// subscribe the create user command
        /// </summary>
        /// <param name="command">create user</param>
        /// <returns></returns>
        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user: '{command.Email}' with name: '{command.Name}'.");
            try 
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
                _logger.LogInformation($"User: '{command.Email}' was created with name: '{command.Name}'.");

                return;
            }
            catch (ProjectInsightException ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(command.Email,
                    ex.Message, ex.Code)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(command.Email,
                    ex.Message, "error"));                
            }
        }
    }
}