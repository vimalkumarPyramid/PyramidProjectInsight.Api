using System.Threading.Tasks;

namespace Pyramid.ProjectInsight.Common.Mongo
{
    /// <summary>
    /// class for check seeding
    /// </summary>
    public interface IDatabaseSeeder
    {
        /// <summary>
        /// seed the database
        /// </summary>
        /// <returns></returns>
         Task SeedAsync();
    }
}