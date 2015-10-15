using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepenteTest.SoccerFan.Web.Models
{
    public class UpdateTitularRequest
    {
        public Domain.Titular.SaveCommand SaveCommand { get; set; }
        public Domain.Address.Data Address { get; set; }
        public int TitularId { get; set; }
    }
}