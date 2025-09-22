using ConsoleAppAirLineManagement.Model;
using ConsoleAppAirLineManagement.Repository;
using ConsoleAppAirLineManagement.Service;
using ConsoleAppAirLineManagement.Utility;
using System;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement
{
    //create class
    public class FlyWithMe
    {
        //main method
        static void Main(string[] args)
        {
            // Initialize services
            IAdminRepository adminRepo = new AdminRepositoryImpl();
            AdminServiceImpl adminService = new AdminServiceImpl(adminRepo);

            IFlightRepository flightRepo = new FlightRepositoryImpl();
            FlightServiceImpl flightService = new FlightServiceImpl(flightRepo);

            IAirportRepository airportRepo = new AirportRepositoryImpl();
            AirportServiceImpl airportService = new AirportServiceImpl(airportRepo);

            // ----- ADMIN LOGIN -----
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("----- FLYWITHME AIRLINE ADMIN LOGIN -----");
            Console.ResetColor();
//username
            string username;
            do
            {
                Console.Write("Username: ");
                username = Console.ReadLine();
                if (!CustomValidation.IsValidUserName(username))
                    Console.WriteLine("Invalid username! Max 15 chars, letters, numbers, _ or . only.");
            } while (!CustomValidation.IsValidUserName(username));
//password
            string password;
            do
            {
                Console.Write("Password: ");
                password = CustomValidation.ReadPassword();
                if (!CustomValidation.IsValidPassword(password))
                    Console.WriteLine("Password must have uppercase, lowercase, digit, special char, min 4 chars.");
            } while (!CustomValidation.IsValidPassword(password));

            if (!adminService.Login(username, password))
            {
                Console.WriteLine("Invalid credentials! Exiting...");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Welcome {username}!");
            Console.ResetColor();

            // ----- FLIGHT DASHBOARD -----
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n----- FLIGHT DASHBOARD -----");
                Console.ResetColor();
                Console.WriteLine("1. List All Flights");
                Console.WriteLine("2. Search Flight By Id");
                Console.WriteLine("3. Add Flight");
                Console.WriteLine("4. Update Flight");
                Console.WriteLine("5. Delete Flight");
                Console.WriteLine("6. Manage Airports");
                Console.WriteLine("7. Exit");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // List All Flights
                        List<Flight> flights = flightService.GetAllFlights();
                        DisplayFlightTable(flights, airportService);
                        break;

                    case "2": // Search Flight
                        int searchId = ReadPositiveInt("Enter Flight Id: ");
                        var flight = flightService.GetFlightById(searchId);
                        if (flight != null)
                            DisplayFlightTable(new List<Flight> { flight }, airportService);
                        else
                            Console.WriteLine("Flight not found!");
                        break;

                    case "3": // Add Flight
                        AddFlight(flightService, airportService);
                        break;

                    case "4": // Update Flight
                        UpdateFlight(flightService, airportService);
                        break;

                    case "5": // Delete Flight
                        int deleteId = ReadPositiveInt("Enter Flight Id to delete: ");
                        Console.Write("Are you sure you want to delete this flight? (y/n): ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            flightService.DeleteFlight(deleteId);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Flight deleted successfully!");
                            Console.ResetColor();
                        }
                        break;

                    case "6": // Manage Airports
                        ManageAirports(airportService);
                        break;

                    case "7": // Exit
                        Console.WriteLine("Exiting application...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        // ----- Add Flight -----
        private static void AddFlight(FlightServiceImpl flightService, AirportServiceImpl airportService)
        {
            List<Airport> airports = airportService.GetAllAirports();
            Console.WriteLine("\nAvailable Airports:");
            Console.WriteLine("ID | Code | Name | City | Country");
            foreach (var a in airports)
                Console.WriteLine($"{a.AirportId} | {a.AirportCode} | {a.AirportName} | {a.City} | {a.Country}");

            Flight newFlight = new Flight();
            newFlight.DepAirportId = ReadPositiveInt("DepAirportId: ");
            newFlight.ArrAirportId = ReadPositiveInt("ArrAirportId: ");
            while (newFlight.ArrAirportId == newFlight.DepAirportId)
            {
                Console.WriteLine("Arrival airport cannot be same as departure airport!");
                newFlight.ArrAirportId = ReadPositiveInt("ArrAirportId: ");
            }

            newFlight.DepDate = ReadFutureDate("DepDate (yyyy-MM-dd): ");
            newFlight.DepTime = ReadTime("DepTime (HH:mm): ");

            do
            {
                newFlight.ArrDate = ReadFutureDate("ArrDate (yyyy-MM-dd): ");
                if (newFlight.ArrDate < newFlight.DepDate)
                    Console.WriteLine("Arrival date cannot be before departure date!");
            } while (newFlight.ArrDate < newFlight.DepDate);

            newFlight.ArrTime = ReadTime("ArrTime (HH:mm): ");

            int newFlightId = flightService.AddFlight(newFlight);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Flight added successfully! Flight ID: {newFlightId}");
            Console.ResetColor();
        }

        // ----- Update Flight -----
        private static void UpdateFlight(FlightServiceImpl flightService, AirportServiceImpl airportService)
        {
            int updateId = ReadPositiveInt("Enter Flight Id to update: ");
            var updateFlight = flightService.GetFlightById(updateId);
            if (updateFlight != null)
            {
                List<Airport> airports = airportService.GetAllAirports();
                Console.WriteLine("\nAvailable Airports:");
                Console.WriteLine("ID | Code | Name | City | Country");
                foreach (var a in airports)
                    Console.WriteLine($"{a.AirportId} | {a.AirportCode} | {a.AirportName} | {a.City} | {a.Country}");

                updateFlight.DepAirportId = ReadPositiveInt("New DepAirportId: ");
                updateFlight.ArrAirportId = ReadPositiveInt("New ArrAirportId: ");
                while (updateFlight.ArrAirportId == updateFlight.DepAirportId)
                {
                    Console.WriteLine("Arrival airport cannot be same as departure airport!");
                    updateFlight.ArrAirportId = ReadPositiveInt("New ArrAirportId: ");
                }

                updateFlight.DepDate = ReadFutureDate("New DepDate (yyyy-MM-dd): ");
                updateFlight.DepTime = ReadTime("New DepTime (HH:mm): ");

                do
                {
                    updateFlight.ArrDate = ReadFutureDate("New ArrDate (yyyy-MM-dd): ");
                    if (updateFlight.ArrDate < updateFlight.DepDate)
                        Console.WriteLine("Arrival date cannot be before departure date!");
                } while (updateFlight.ArrDate < updateFlight.DepDate);

                updateFlight.ArrTime = ReadTime("New ArrTime (HH:mm): ");

                flightService.UpdateFlight(updateFlight);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Flight updated successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Flight not found!");
            }
        }

        // ----- Manage Airports -----
        private static void ManageAirports(AirportServiceImpl airportService)
        {
            while (true)
            {
                Console.WriteLine("\n----- AIRPORT DASHBOARD -----");
                Console.WriteLine("1. List All Airports");
                Console.WriteLine("2. Add Airport");
                Console.WriteLine("3. Update Airport");
                Console.WriteLine("4. Delete Airport");
                Console.WriteLine("5. Back to Flight Dashboard");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                        //get all airports
                    case "1":
                        List<Airport> airports = airportService.GetAllAirports();
                        Console.WriteLine("ID | Code | Name | City | Country");
                        foreach (var a in airports)
                            Console.WriteLine($"{a.AirportId} | {a.AirportCode} | {a.AirportName} | {a.City} | {a.Country}");
                        break;
//add new airport
                    case "2":
                        Airport newAirport = new Airport();
                        Console.Write("Airport Code: ");
                        newAirport.AirportCode = Console.ReadLine();
                        Console.Write("Airport Name: ");
                        newAirport.AirportName = Console.ReadLine();
                        Console.Write("City: ");
                        newAirport.City = Console.ReadLine();
                        Console.Write("Country: ");
                        newAirport.Country = Console.ReadLine();
                        airportService.AddAirport(newAirport);
                        Console.WriteLine("Airport added successfully!");
                        break;
//update airport
                    case "3":
                        int updateId = ReadPositiveInt("Enter Airport ID to update: ");
                        var airportToUpdate = airportService.GetAirportById(updateId);
                        if (airportToUpdate != null)
                        {
                            Console.Write("Airport Code: ");
                            airportToUpdate.AirportCode = Console.ReadLine();
                            Console.Write("Airport Name: ");
                            airportToUpdate.AirportName = Console.ReadLine();
                            Console.Write("City: ");
                            airportToUpdate.City = Console.ReadLine();
                            Console.Write("Country: ");
                            airportToUpdate.Country = Console.ReadLine();
                            airportService.UpdateAirport(airportToUpdate);
                            Console.WriteLine("Airport updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Airport not found!");
                        }
                        break;
//delete airport
                    case "4":
                        int deleteId = ReadPositiveInt("Enter Airport ID to delete: ");
                        airportService.DeleteAirport(deleteId);
                        Console.WriteLine("Airport deleted successfully!");
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        // ----- Helper Methods -----
        private static int ReadPositiveInt(string prompt)
        {
            int value;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out value) || value <= 0);
            return value;
        }

        private static DateTime ReadFutureDate(string prompt)
        {
            DateTime date;
            do
            {
                Console.Write(prompt);
                while (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.WriteLine("Invalid date format! Try again.");
                    Console.Write(prompt);
                }

                if (date < DateTime.Today)
                    Console.WriteLine("Date must be today or a future date!");
            } while (date < DateTime.Today);

            return date;
        }

        private static TimeSpan ReadTime(string prompt)
        {
            TimeSpan time;
            do
            {
                Console.Write(prompt);
            } while (!TimeSpan.TryParse(Console.ReadLine(), out time));
            return time;
        }

        private static void DisplayFlightTable(List<Flight> flights, AirportServiceImpl airportService)
        {
            if (flights.Count == 0)
            {
                Console.WriteLine("No flights available.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nFlightId | DepAirport | ArrAirport | DepDate | DepTime | ArrDate | ArrTime");
            Console.ResetColor();

            foreach (var f in flights)
            {
                var depAirport = airportService.GetAirportById(f.DepAirportId)?.AirportCode ?? "N/A";
                var arrAirport = airportService.GetAirportById(f.ArrAirportId)?.AirportCode ?? "N/A";

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{f.FlightId,7} | {depAirport,10} | {arrAirport,10} | {f.DepDate:yyyy-MM-dd} | {f.DepTime} | {f.ArrDate:yyyy-MM-dd} | {f.ArrTime}");
            }
            Console.ResetColor();
        }
    }
}
