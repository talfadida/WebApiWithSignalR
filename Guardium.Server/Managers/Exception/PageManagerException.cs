using System;
using System.Runtime.Serialization;

namespace Guardium.Server
{
    [Serializable]
    public class PageManagerException : Exception
    {
        public PageManagerException()
        {
        }

        public PageManagerException(string message) : base(message)
        {
        }

        public PageManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
         
    }
}