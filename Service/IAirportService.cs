using ConsoleAppAirLineManagement.Model;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Service
{
    public interface IAirportService
    {
        List<Airport> GetAllAirports();
        Airport GetAirportById(int airportId);
        void AddAirport(Airport airport);
        void UpdateAirport(Airport airport);
        void DeleteAirport(int airportId);
    }
}

