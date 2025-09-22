using ConsoleAppAirLineManagement.Model;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Service
{
    public interface IFlightService
    {
        List<Flight> GetAllFlights();
        Flight GetFlightById(int id);

        // Change return type to int
        int AddFlight(Flight flight);

        void UpdateFlight(Flight flight);
        void DeleteFlight(int id);

        List<Airport> GetAllAirports();
    }

}
