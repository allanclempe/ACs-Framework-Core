using System;
using ACs.ErrorHandler.AspnetCore;
using ACs.Misc;

namespace ACs.Framework.Web.Core.Infra
{
    public class SystemLogicException : ErrorHandlerException
    {
        public ExceptionMessage MessageType => (ExceptionMessage)ErrorCode;

        public SystemLogicException(ExceptionMessage message)
            : base((int)message, message.GetDescription())
        {
        }

        public SystemLogicException(ExceptionMessage message, params string[] parameters)
            : base((int)message, message.GetDescription(parameters))
        {
        }

        public SystemLogicException(string message)
            : base(message)
        {
        }

    }
}
