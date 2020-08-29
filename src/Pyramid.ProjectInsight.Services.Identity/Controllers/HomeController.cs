using Microsoft.AspNetCore.Mvc;

namespace Pyramid.ProjectInsight.Services.Identity.Controllers
{
    /// <summary>
    /// default controller
    /// </summary>
    [Route("")]
    public class HomeController : Controller
    {
        /// <summary>
        /// it will show message while api load
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from Pyramid ProjectInsight Identity API!");
    }
}