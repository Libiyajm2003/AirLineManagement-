using ConsoleAppAirLineManagement.Model;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Service
{
//create interface of IAirportService
    public interface IAirportService
    {
        List<Airport> GetAllAirports(); //get all airports
        Airport GetAirportById(int airportId);// get aiports by id
        void AddAirport(Airport airport); //add airports
        void UpdateAirport(Airport airport);// update airports
        void DeleteAirport(int airportId); //delete airports
    }
}

