using Pyramid.ProjectInsight.Common.Auth;
using System.Threading.Tasks;

namespace Pyramid.ProjectInsight.Services.Identity.Services
{
    public interface IUserService
    {
         Task RegisterAsync(string email, string password, string name);
         Task<JsonWebToken> LoginAsync(string email, string password);
    }
}