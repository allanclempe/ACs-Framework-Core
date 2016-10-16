using ACs.NHibernate.Generic;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ACs.NHibernate.AspnetCore
{
    
    public class SessionRequiredAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public bool OpenTransaction { get; set; }
        public bool ModelStateErrorChecker { get; set; }
        private IDatabaseRequest _request;
        private IDatabaseFactory _factory;
	    public TransactionIsolationLevel? IsolationLevel;

	    public SessionRequiredAttribute(TransactionIsolationLevel isolationLevel)
			:this()
	    {
			IsolationLevel = isolationLevel;
			
		}

	    public SessionRequiredAttribute()
        {
			OpenTransaction = true;
			ModelStateErrorChecker = true;
		}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            _factory = (IDatabaseFactory)filterContext.HttpContext.RequestServices.GetService(typeof (IDatabaseFactory));

            _request = _factory.BeginRequest(OpenTransaction, IsolationLevel);
            
             
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);

            if (context.HttpContext.Response.StatusCode != 200 || context.Exception != null || (ModelStateErrorChecker && context.ModelState.ErrorCount > 0))
            {
                _request.Finish(true);
                return;
            }

           _request.Finish();

        }

        public void OnException(ExceptionContext context)
        {
            _request?.Finish(true);

          //  throw context.Exception;
        }
    }
}
