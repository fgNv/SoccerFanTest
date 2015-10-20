using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;

namespace NepenteTest.SoccerFan.Web.Exceptions
{
    public static class ExceptionHandling
    {
        private static HttpResponseMessage BuildContent(IEnumerable<string> messages)
        {
            var exceptionMessage = new ExceptionResponse { Messages = messages };
            var formatter = new JsonMediaTypeFormatter();
            var content = new ObjectContent<ExceptionResponse>(exceptionMessage, formatter, "application/json");
            return new HttpResponseMessage { Content = content };
        }

        public static void Handle(this Exception e, HttpActionExecutedContext context)
        {
            var response = BuildContent(new[] { e.Message });
            context.Response = response;
        }

        public static void Handle(this OperationResultException e, HttpActionExecutedContext context)
        {
            var response = BuildContent(e.Errors);
            context.Response = response;
        }
    }
}