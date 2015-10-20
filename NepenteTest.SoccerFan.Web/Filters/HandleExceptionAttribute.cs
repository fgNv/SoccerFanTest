using NepenteTest.SoccerFan.Web.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace NepenteTest.SoccerFan.Web.Filters
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            ExceptionHandling.Handle(context.Exception as dynamic, context);
        }
    }
}