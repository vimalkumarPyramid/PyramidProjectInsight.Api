using Pyramid.ProjectInsight.Services.Activities.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Pyramid.ProjectInsight.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);
        Task AddAsync(Activity activity);
        Task DeleteAsync(Guid id);
    }
}