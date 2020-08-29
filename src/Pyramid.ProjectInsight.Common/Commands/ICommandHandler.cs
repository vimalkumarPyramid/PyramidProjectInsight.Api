using System.Threading.Tasks;

namespace Pyramid.ProjectInsight.Common.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
         Task HandleAsync(T command);
    }
}