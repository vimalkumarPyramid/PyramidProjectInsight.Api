using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pyramid.ProjectInsight.Common.Commands;
using Pyramid.ProjectInsight.Services.Identity.Services;

namespace Pyramid.ProjectInsight.Services.Identity.Controllers
{
    /// <summary>
    /// class for account
    /// </summary>
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// constructot for initialize user service
        /// </summary>
        /// <param name="userService">user service</param>
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// login user
        /// </summary>
        /// <param name="command">command</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}