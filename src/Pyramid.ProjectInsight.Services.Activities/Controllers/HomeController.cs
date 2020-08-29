using Microsoft.AspNetCore.Mvc;

namespace Pyramid.ProjectInsight.Services.Activities.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from Pyramid Insight Activites API!");
    }
}