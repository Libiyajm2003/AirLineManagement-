using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAirLineManagement.Model
{
        public class Airport
        {
            public int AirportId { get; set; }
            public string AirportCode { get; set; }
            public string AirportName { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
    }
