using ConsoleAppAirLineManagement.Model;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Repository
{
//Interface for Flight repository to handle data access related to flights
    public interface IFlightRepository
    {
        //Retrieves all flights from the database
        List<Flight> GetAllFlights();
        Flight GetFlightById(int flightId); //get flight by id

        // Change return type to int
        int AddFlight(Flight flight); //add flight

        void UpdateFlight(Flight flight); // update flight
        void DeleteFlight(int flightId);// delete flight

        List<Airport> GetAllAirports();
    }

}

