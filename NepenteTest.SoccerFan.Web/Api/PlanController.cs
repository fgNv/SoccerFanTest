using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static Domain;
using NepenteTest.SoccerFan.Interop;

namespace NepenteTest.SoccerFan.Web.Api
{
    public class PlanController : ApiController
    {
        private readonly PlanService _planService;

        public PlanController()
        {
            _planService = new PlanService();
        }

        public IEnumerable<Plan> Get()
        {
            return _planService.GetAll();
        }
    }
}