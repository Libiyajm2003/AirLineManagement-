using ConsoleAppAirLineManagement.Model;
using ConsoleAppAirLineManagement.Repository;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Service
{
//Implement the interface of IFlightService
    public class FlightServiceImpl : IFlightService
    {
//Fields
        private readonly IFlightRepository _flightRepo;
//Constructors
        public FlightServiceImpl(IFlightRepository flightRepo)
        {
            _flightRepo = flightRepo;
        }
//Methods
        // --- Flights ---
        public List<Flight> GetAllFlights()
        {
            return _flightRepo.GetAllFlights();
        }

        public Flight GetFlightById(int id)
        {
            return _flightRepo.GetFlightById(id);
        }

        public int AddFlight(Flight flight)
        {
            return _flightRepo.AddFlight(flight);
        }


        public void UpdateFlight(Flight flight)
        {
            _flightRepo.UpdateFlight(flight);
        }

        public void DeleteFlight(int id)
        {
            _flightRepo.DeleteFlight(id);
        }

        // --- Airports ---
        public List<Airport> GetAllAirports()
        {
            return _flightRepo.GetAllAirports();
        }
    }
}
