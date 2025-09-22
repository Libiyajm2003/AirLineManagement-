using ConsoleAppAirLineManagement.Model;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Repository
{
    public interface IFlightRepository
    {
        List<Flight> GetAllFlights();
        Flight GetFlightById(int flightId);

        // Change return type to int
        int AddFlight(Flight flight);

        void UpdateFlight(Flight flight);
        void DeleteFlight(int flightId);

        List<Airport> GetAllAirports();
    }

}

