using ConsoleAppAirLineManagement.Model;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Service
{
//create interface
    public interface IFlightService
    {
        List<Flight> GetAllFlights(); // list all the flight 
        Flight GetFlightById(int id); // list fight by id

        // Change return type to int
        int AddFlight(Flight flight);// add flight details

        void UpdateFlight(Flight flight); //update flight details
        void DeleteFlight(int id); //delete flight details

        List<Airport> GetAllAirports();
    }

}
