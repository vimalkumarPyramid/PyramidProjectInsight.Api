using System.Threading.Tasks;

namespace Pyramid.ProjectInsight.Common.Mongo
{
    /// <summary>
    /// interface for handle mongodb initialization
    /// </summary>
    public interface IDatabaseInitializer
    {
        /// <summary>
        /// intialize mongo db
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
    }
}