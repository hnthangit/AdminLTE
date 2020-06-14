using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayer.SqlServer
{
    public class CountryDAL : ICountryDAL
    {
        private string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Danh sách các đất nước
        /// </summary>
        /// <returns></returns>
        public List<Country> ListCountries()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection connection = new SqlConnection(connectionString)) //Tạo đối tượng kết nối CSDL
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())//Thực hiện truy vấn CSDL
                {
                    cmd.CommandText = @"select * from Countries";//Câu truy vấn
                    cmd.CommandType = System.Data.CommandType.Text; //Cho biết lệnh mà ta sd là lệnh gì
                    cmd.Connection = connection;//lệnh kết nối;
                    using (SqlDataReader dbreader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (dbreader.Read())
                        {
                            Country country = new Country();
                            country.CountryName = Convert.ToString(dbreader["Country"]);
                            data.Add(country);
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }
    }
}