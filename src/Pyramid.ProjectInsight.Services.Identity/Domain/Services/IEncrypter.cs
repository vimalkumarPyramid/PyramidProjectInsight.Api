namespace Pyramid.ProjectInsight.Services.Identity.Domain.Services
{
    /// <summary>
    /// interface for encrypt
    /// </summary>
    public interface IEncrypter
    {
        /// <summary>
        /// get salt
        /// </summary>
        /// <returns></returns>
        string GetSalt();

        /// <summary>
        /// encrypt value
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="salt">salt</param>
        /// <returns></returns>
        string GetHash(string value, string salt);
    }
}