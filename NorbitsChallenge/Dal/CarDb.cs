using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Dal
{
    public class CarDb
    {
        private readonly IConfiguration _config;

        public CarDb(IConfiguration config)
        {
            _config = config;
        }

        public int GetTireCount(int companyId, string licensePlate)
        {
            int result = 0;

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand {Connection = connection, CommandType = CommandType.Text})
                {
                    command.CommandText = $"select * from car where companyId = {companyId} and licenseplate = '{licensePlate}'";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = (int) reader["tireCount"];
                        }
                    }
                }
            }

            return result;
        }

        public List<Car> GetAllCarsById(int CompanyId)
        {
            var Cars = new List<Car>();
            var connectionString = _config.GetSection("connectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where CompanyId = {CompanyId}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car temporaryCar = new Car();
                            temporaryCar.LicensePlate = (string)reader["LicensePlate"];
                            temporaryCar.Description = (string)reader["Description"];
                            temporaryCar.Brand = (string)reader["Brand"];
                            temporaryCar.TireCount = (int)reader["TireCount"];
                            temporaryCar.Model = (string)reader["Model"];

                            Cars.Add(temporaryCar);
                        }
                    }
                }
            }
            return Cars;
        }

        public List<Car> GetAllCars()
        {
            var Cars = new List<Car>();
            var connectionString = _config.GetSection("connectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car temporaryCar = new Car();
                            temporaryCar.LicensePlate = (string)reader["LicensePlate"];
                            temporaryCar.Description = (string)reader["Description"];
                            temporaryCar.Brand = (string)reader["Brand"];
                            temporaryCar.TireCount = (int)reader["TireCount"];
                            temporaryCar.Model = (string)reader["Model"];
                            temporaryCar.CompanyID = (int)reader["CompanyID"];

                            Cars.Add(temporaryCar);
                        }
                    }
                }
            }
            return Cars;
        }

        public Car GetSpecificCar(string LicensePlate)
        {
            var thisCar = new Car();
            var connectionString = _config.GetSection("connectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where LicensePlate = '{LicensePlate}'";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                           thisCar.LicensePlate = (string)reader["LicensePlate"];
                           thisCar.Description = (string)reader["Description"];
                           thisCar.Brand = (string)reader["Brand"];
                           thisCar.TireCount = (int)reader["TireCount"];
                           thisCar.Model = (string)reader["Model"];
                           thisCar.CompanyID = (int)reader["CompanyID"];

         
                        }
                    }
                }
            }
            return thisCar;
        }
        public void AddCar(string LicensePlate, string Description, string Model, string Brand, int TireCount, int CompanyId)
        {
            var connectionString = _config.GetSection("connectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"Insert into Car Values ('{LicensePlate}', '{Description}', '{Model}', '{Brand}', {TireCount}, {CompanyId})";

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCar(string LicensePlate)
        {
            var connectionString = _config.GetSection("connectionString").Value;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"Delete from Car where LicensePlate = '{LicensePlate}'";

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCar(string LicensePlate, string Description, string Model, string Brand, int TireCount, int CompanyID)
        {
            var connectionString = _config.GetSection("connectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"Update Car set Description = '{Description}', Model = '{Model}', Brand = '{Brand}', TireCount = {TireCount}, CompanyId = {CompanyID} where LicensePlate = '{LicensePlate}' ";

                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
