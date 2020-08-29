using Microsoft.AspNetCore.Mvc;

namespace Pyramid.ProjectInsight.Api.Controllers
{
    /// <summary>
    /// default class
    /// </summary>
    [Route("")]
    public class HomeController : Controller
    {
        /// <summary>
        /// default controller
        /// </summary>
        /// <returns>it will return default value</returns>
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from Pyramid Insight API!");
    }
}