using NepenteTest.SoccerFan.Interop;
using NepenteTest.SoccerFan.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static Infrastructure;

namespace NepenteTest.SoccerFan.Web.Api
{
    public class TitularController : ApiController
    {
        private readonly TitularService _titularService;

        public TitularController(/*TitularService titularService*/)
        {
            _titularService = new TitularService();
        }

        public IEnumerable<Domain.Titular.Data> GetAll()
        {
            return _titularService.GetAll();
        }

        [HttpPost]
        public void Create([FromBody]Domain.Titular.SaveCommand request)
        {
            var result = _titularService.Create(request);
            if (!result.IsError)
                return;

            var messages = (result as OperationResult.Error).Item;
            throw new Exception(messages.FirstOrDefault());
        }

        [HttpPut]
        public void Update(UpdateTitularRequest request)
        {
            _titularService.Update(request.TitularId, request.SaveCommand);
        }
    }
}