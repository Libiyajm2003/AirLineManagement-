using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAirLineManagement.Model
{
//create flight class
    public class Flight
    {
        //give fields / properties
        public int FlightId { get; set; }
        public int DepAirportId { get; set; }
        public int ArrAirportId { get; set; }
        public DateTime DepDate { get; set; }
        public TimeSpan DepTime { get; set; }
        public DateTime ArrDate { get; set; }
        public TimeSpan ArrTime { get; set; }
    }
}
