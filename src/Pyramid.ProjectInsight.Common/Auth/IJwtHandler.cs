using System;

namespace Pyramid.ProjectInsight.Common.Auth
{
    public interface IJwtHandler
    {
        /// <summary>
        /// create token
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>return token</returns>
        JsonWebToken Create(Guid userId);     
    }
}