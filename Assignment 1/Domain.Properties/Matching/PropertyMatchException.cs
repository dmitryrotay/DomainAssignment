using System;

namespace Domain.Properties.Matching
{
    public class PropertyMatchException : ApplicationException
    {
        public PropertyMatchException(string message, Exception innerException)
            : base(message, innerException)
        {}
    }
}
