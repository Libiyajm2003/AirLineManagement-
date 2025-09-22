using ConsoleAppAirLineManagement.Model;
using ConsoleAppAirLineManagement.Repository;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Service
{
    public class AirportServiceImpl : IAirportService
    {
        private readonly IAirportRepository _airportRepo;

        public AirportServiceImpl(IAirportRepository airportRepo)
        {
            _airportRepo = airportRepo;
        }

        public List<Airport> GetAllAirports()
        {
            return _airportRepo.GetAllAirports();
        }

        public Airport GetAirportById(int airportId)
        {
            return _airportRepo.GetAirportById(airportId);
        }

        public void AddAirport(Airport airport)
        {
            _airportRepo.AddAirport(airport);
        }

        public void UpdateAirport(Airport airport)
        {
            _airportRepo.UpdateAirport(airport);
        }

        public void DeleteAirport(int airportId)
        {
            _airportRepo.DeleteAirport(airportId);
        }
    }
}
