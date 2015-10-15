using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepenteTest.SoccerFan.Web.Models
{
    public class CreateTitularRequest
    {
        public Domain.Titular.Data Titular { get; set; }
        public Domain.Address.Data Address { get; set; }
    }

}