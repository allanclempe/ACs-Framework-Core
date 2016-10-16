using System;

namespace ACs.ErrorHandler.AspnetCore
{
    //todo: find System.Security on .net core 5.4

    public class ErrorHandlerException : Exception
    {
        public int ErrorCode { get; }

        public ErrorHandlerException(string message)
            :base(message)
        {
        }

        public ErrorHandlerException(int errorCode, string message)
            :base(message)
        {
            ErrorCode = errorCode;
        }


//        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
//        protected ErrorHandlerException(SerializationInfo info, StreamingContext context)
//            : base(info, context)
//        {
//            this.ErrorCode = info.GetInt32("ErrorCode");
//        }
//
//        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
//        public override void GetObjectData(SerializationInfo info, StreamingContext context)
//        {
//            if (info == null)
//                throw new ArgumentNullException(nameof(info));
//
//            info.AddValue("ErrorCode", this.ErrorCode);
//
//            // MUST call through to the base class to let it save its own state
//            base.GetObjectData(info, context);
//
//        }
    }
}
