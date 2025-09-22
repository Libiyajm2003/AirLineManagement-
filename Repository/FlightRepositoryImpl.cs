using ConsoleAppAirLineManagement.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Repository
{
    public class FlightRepositoryImpl : IFlightRepository
    {
        private string connStr = @"Data Source=localhost;Initial Catalog=FlyWithMeDB;Integrated Security=True;TrustServerCertificate=True";

        // --- Flights ---
        public List<Flight> GetAllFlights()
        {
            List<Flight> flights = new List<Flight>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllFlights", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flights.Add(new Flight
                        {
                            FlightId = (int)reader["FlightId"],
                            DepAirportId = (int)reader["DepAirportId"],
                            ArrAirportId = (int)reader["ArrAirportId"],
                            DepDate = (DateTime)reader["DepDate"],
                            DepTime = (TimeSpan)reader["DepTime"],
                            ArrDate = (DateTime)reader["ArrDate"],
                            ArrTime = (TimeSpan)reader["ArrTime"]
                        });
                    }
                }
            }
            return flights;
        }

        public Flight GetFlightById(int id)
        {
            Flight flight = null;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_GetFlightById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FlightId", id);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        flight = new Flight
                        {
                            FlightId = (int)reader["FlightId"],
                            DepAirportId = (int)reader["DepAirportId"],
                            ArrAirportId = (int)reader["ArrAirportId"],
                            DepDate = (DateTime)reader["DepDate"],
                            DepTime = (TimeSpan)reader["DepTime"],
                            ArrDate = (DateTime)reader["ArrDate"],
                            ArrTime = (TimeSpan)reader["ArrTime"]
                        };
                    }
                }
            }
            return flight;
        }

        public int AddFlight(Flight flight)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_AddFlight", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@DepAirportId", flight.DepAirportId);
                cmd.Parameters.AddWithValue("@ArrAirportId", flight.ArrAirportId);
                cmd.Parameters.AddWithValue("@DepDate", flight.DepDate);
                cmd.Parameters.AddWithValue("@DepTime", flight.DepTime);
                cmd.Parameters.AddWithValue("@ArrDate", flight.ArrDate);
                cmd.Parameters.AddWithValue("@ArrTime", flight.ArrTime);

                // Output parameter for new FlightId
                SqlParameter outputId = new SqlParameter("@NewFlightId", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                con.Open();
                cmd.ExecuteNonQuery();

                return (int)outputId.Value;
            }
        }

        public void UpdateFlight(Flight flight)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateFlight", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FlightId", flight.FlightId);
                cmd.Parameters.AddWithValue("@DepAirportId", flight.DepAirportId);
                cmd.Parameters.AddWithValue("@ArrAirportId", flight.ArrAirportId);
                cmd.Parameters.AddWithValue("@DepDate", flight.DepDate);
                cmd.Parameters.AddWithValue("@DepTime", flight.DepTime);
                cmd.Parameters.AddWithValue("@ArrDate", flight.ArrDate);
                cmd.Parameters.AddWithValue("@ArrTime", flight.ArrTime);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteFlight(int id)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteFlight", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FlightId", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // --- Airports ---
        public List<Airport> GetAllAirports()
        {
            List<Airport> airports = new List<Airport>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT AirportId, AirportCode, AirportName, City, Country FROM TblAirport ORDER BY AirportId";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        airports.Add(new Airport
                        {
                            AirportId = (int)reader["AirportId"],
                            AirportCode = reader["AirportCode"].ToString(),
                            AirportName = reader["AirportName"].ToString(),
                            City = reader["City"].ToString(),
                            Country = reader["Country"].ToString()
                        });
                    }
                }
            }

            return airports;
        }
    }
}
