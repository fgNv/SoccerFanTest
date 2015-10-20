using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Infrastructure.OperationResult;

namespace NepenteTest.SoccerFan.Web.Exceptions
{
    public class OperationResultException : Exception
    {
        public IEnumerable<string> Errors { get; private set; }

        public OperationResultException(Error error)
        {
            Errors = error.Item;
        }
    }
}