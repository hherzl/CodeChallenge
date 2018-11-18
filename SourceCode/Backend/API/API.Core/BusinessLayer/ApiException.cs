using System;

namespace API.Core.BusinessLayer
{
    public class ApiException : Exception
    {
        public ApiException()
            : base()
        {
        }

        public ApiException(string message)
            : base(message)
        {
        }
    }
}
