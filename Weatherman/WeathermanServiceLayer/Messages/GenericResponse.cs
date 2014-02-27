using System;

namespace WeathermanServiceLayer.Messages
{
    public class GenericResponse
    {
        public GenericResponse()
        {
            Success = false;
            Message = string.Empty;
        }

        public Exception Exception { get; set; }

        public bool Success
        { get; set; }

        public string Message
        { get; set; }
    }
}
