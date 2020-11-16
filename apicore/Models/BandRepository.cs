using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace apicore.Models
{
    public class BandRepository : IBandRepository
    {

        MySqlConnection connection;

        public BandRepository()
        {
            string MyConnectString = "server=localhost;user id=bandsys;password=p@ssword;database=bands";

            connection = new MySqlConnection(MyConnectString);

            connection.Open();


        }

        public Band AddBand(Band band)
        {

            try
            {
                GetBand(band.Name);
            }
            catch (Exception e)
            {
                throw e;
            }




            using (var cmd = new MySqlCommand())
            {
                int rows = 0;
                cmd.Connection = connection;
                cmd.CommandText = "INSERT into band(Name,Year,Rating) VALUES(@n,@y,@r)";
                cmd.Parameters.AddWithValue("n", band.Name);
                cmd.Parameters.AddWithValue("y", band.Year);
                cmd.Parameters.AddWithValue("r", band.Rating);
                rows = cmd.ExecuteNonQuery();
                if (rows == 1)
                {
                    return band;
                }

                if (rows > 0)
                {

                }
            }


            return band;
        }

        public bool DeleteBand(string name)
        {
            int rows = 0;

            using (var command = new MySqlCommand("DELETE FROM band WHERE Name = @name", connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Prepare();

                rows = command.ExecuteNonQuery();
                if (rows == 1)
                {
                    return true;
                }

            }

            return false;
        }

        public IEnumerable<Band> GetAllBands()
        {
            List<Band> bands = new List<Band>();



            using (var command = new MySqlCommand("SELECT Name, Year, Rating from band;", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var band = new Band();
                        band.Name = reader.GetString(0);
                        band.Year = reader.GetInt32(1);
                        band.Rating = reader.GetUInt16(2);
                        bands.Add(band);
                    }
                }
            }

            return bands;
        }

        public IEnumerable<Band> GetBandByQuery(int rating, int year)
        {
            return null;
        }

        public Band GetBand(string name)
        {
            List<Band> bands = new List<Band>();

            using (var command = new MySqlCommand("SELECT Name, Year, Rating from band WHERE Name = @n;", connection))
            {
                command.Parameters.AddWithValue("@n", name);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var band = new Band();
                        band.Name = reader.GetString(0);
                        band.Year = reader.GetInt32(1);
                        band.Rating = reader.GetUInt16(2);
                        bands.Add(band);
                    }
                }
            }

            if (bands.Count == 1)
            {
                return bands[0];
            }
            else if (bands.Count > 1)
            {
                return null;
            }

            return null;

        }

        public Band UpdateBand(Band band)
        {
            int rows = 0;

            using (var command = new MySqlCommand("UPDATE band SET Year=@year, Rating = @rating WHERE Name = @name;", connection))
            {
                command.Parameters.AddWithValue("@name", band.Name);
                command.Parameters.AddWithValue("@year", band.Year);
                command.Parameters.AddWithValue("@rating", band.Rating);
                rows = command.ExecuteNonQuery();
                if (rows == 1)
                {
                    return band;
                }
            }

            return null;
        }

        public Band UpdateRating(string name, int rating)
        {
            int rows = 0;

            using (var command = new MySqlCommand("UPDATE band SET Rating = @rating WHERE Name = @name;", connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@rating", rating);
                rows = command.ExecuteNonQuery();
                if (rows == 1)
                {
                    return GetBand(name);
                }
            }

            return null;

        }


    }
}
