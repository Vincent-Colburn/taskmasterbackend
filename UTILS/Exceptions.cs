using System;

namespace taskmasterbackend.Exceptions
{
    public class NotAuthorized : Exception
    {
        public NotAuthorized(string message) : base(message)
        {
        }
    }
}