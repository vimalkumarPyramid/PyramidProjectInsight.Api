using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pyramid.ProjectInsight.Common.Commands;
using RawRabbit;

namespace Pyramid.ProjectInsight.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController: Controller
    {
        private readonly IBusClient _busClient;

        public UsersController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            try
            {
                await _busClient.PublishAsync(command);
            }
            catch(Exception ex)
            {

            }

            return Accepted();
        }
    }
}