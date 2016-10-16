//using System;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//
//namespace ACs.ErrorHandler.AspnetCore
//{
//    public class ErrorHandleFilter<T> : IExceptionFilter where T : ErrorHandlerException
//    {
//        public void OnException(ExceptionContext context)
//        {
//            var error = context.Exception as T;
//
//            if (error == null) return;
//
//            if (error.ErrorCode == 404)
//            {
//                context.Result = new NotFoundResult();
//                return;
//            }
//
//            context.Result = new BadRequestObjectResult(error);
//        }
//    }
//}
