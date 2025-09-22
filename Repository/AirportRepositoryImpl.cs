using ConsoleAppAirLineManagement.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ConsoleAppAirLineManagement.Repository
{
    public class AirportRepositoryImpl : IAirportRepository
    {
        private string connStr = @"Data Source=localhost;Initial Catalog=FlyWithMeDB;Integrated Security=True;TrustServerCertificate=True";

        public List<Airport> GetAllAirports()
        {
            List<Airport> airports = new List<Airport>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM TblAirport", con);
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

        public Airport GetAirportById(int airportId)
        {
            Airport airport = null;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM TblAirport WHERE AirportId=@id", con);
                cmd.Parameters.AddWithValue("@id", airportId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        airport = new Airport
                        {
                            AirportId = (int)reader["AirportId"],
                            AirportCode = reader["AirportCode"].ToString(),
                            AirportName = reader["AirportName"].ToString(),
                            City = reader["City"].ToString(),
                            Country = reader["Country"].ToString()
                        };
                    }
                }
            }
            return airport;
        }

        public void AddAirport(Airport airport)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO TblAirport (AirportCode, AirportName, City, Country) VALUES (@code,@name,@city,@country)", con);
                cmd.Parameters.AddWithValue("@code", airport.AirportCode);
                cmd.Parameters.AddWithValue("@name", airport.AirportName);
                cmd.Parameters.AddWithValue("@city", airport.City);
                cmd.Parameters.AddWithValue("@country", airport.Country);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAirport(Airport airport)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE TblAirport SET AirportCode=@code, AirportName=@name, City=@city, Country=@country WHERE AirportId=@id", con);
                cmd.Parameters.AddWithValue("@code", airport.AirportCode);
                cmd.Parameters.AddWithValue("@name", airport.AirportName);
                cmd.Parameters.AddWithValue("@city", airport.City);
                cmd.Parameters.AddWithValue("@country", airport.Country);
                cmd.Parameters.AddWithValue("@id", airport.AirportId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAirport(int airportId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM TblAirport WHERE AirportId=@id", con);
                cmd.Parameters.AddWithValue("@id", airportId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
