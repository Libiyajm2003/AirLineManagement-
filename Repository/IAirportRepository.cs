using ConsoleAppAirLineManagement.Model;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Repository
{
//Interface for Airport repository to handle data access related to airports
    public interface IAirportRepository
    {
        //Retrieves all airports from the database
        List<Airport> GetAllAirports();
        Airport GetAirportById(int airportId); //get by id
        void AddAirport(Airport airport);// add the airport
        void UpdateAirport(Airport airport); //update the airport
        void DeleteAirport(int airportId); // delete the airport
    }
}
