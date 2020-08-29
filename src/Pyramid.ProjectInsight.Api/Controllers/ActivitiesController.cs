using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pyramid.ProjectInsight.Api.Repositories;
using Pyramid.ProjectInsight.Common.Commands;
using RawRabbit;

namespace Pyramid.ProjectInsight.Api.Controllers
{
    /// <summary>
    /// It is used to create and get activities
    /// </summary>
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : Controller
    {
        #region Private Variables
        /// <summary>
        /// Rabbitmq client for publish on queue
        /// </summary>
        private readonly IBusClient _busClient;
        /// <summary>
        /// Active repositiory
        /// </summary>
        private readonly IActivityRepository _repository;
        #endregion

        #region Constructor
        /// <summary>
        /// Intialize the bus client and active repository
        /// </summary>
        /// <param name="busClient">busclient for publish message</param>
        /// <param name="repository">active repositiory</param>
        public ActivitiesController(IBusClient busClient,
            IActivityRepository repository)
        {
            _busClient = busClient;
            _repository = repository;
        }
        #endregion

        /// <summary>
        /// browse activity by userid
        /// </summary>
        /// <returns>it will return search activity</returns>
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var activities = await _repository
                .BrowseAsync(Guid.Parse(User.Identity.Name));

            return Json(activities.Select(x => new {x.Id, x.Name, x.Category, x.CreatedAt}));
        }

        /// <summary>
        /// get activity by activity id
        /// </summary>
        /// <param name="id">activity id</param>
        /// <returns>it will return activity</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = await _repository.GetAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            if (activity.UserId != Guid.Parse(User.Identity.Name))
            {
                return Unauthorized();
            }

            return Json(activity);
        }

        /// <summary>
        /// got post activity
        /// </summary>
        /// <param name="command">activity</param>
        /// <returns>it will return http status</returns>
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.UserId = Guid.Parse(User.Identity.Name);
            command.CreatedAt = DateTime.UtcNow;
            await _busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }
    }
}