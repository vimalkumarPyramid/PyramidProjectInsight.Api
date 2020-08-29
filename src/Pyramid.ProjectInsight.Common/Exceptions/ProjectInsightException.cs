using System;

namespace Pyramid.ProjectInsight.Common.Exceptions
{
    /// <summary>
    /// class for handle exception
    /// </summary>
    public class ProjectInsightException : Exception
    {
        public string Code { get; }

        public ProjectInsightException()
        {
        }

        public ProjectInsightException(string code)
        {
            Code = code;
        }

        public ProjectInsightException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public ProjectInsightException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public ProjectInsightException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public ProjectInsightException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}