using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayer.SqlServer
{
    /// <summary>
    /// Triển khai hàm từ interface Shipper
    /// </summary>
    public class ShipperDAL : IShipperDAL
    {
        private string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public ShipperDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Bổ sung thông tin người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Shipper data)
        {
            int shipperId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Shippers
                                        (
                                        CompanyName,
                                        Phone
                                        )
                                        VALUES
                                        (
                                        @CompanyName,
                                        @Phone
                                        );
                                        SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                shipperId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return shipperId;
        }

        /// <summary>
        /// Đếm số lượng kết quả tìm kiếm được
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <returns>Trả về số lượng kết quả tìm được</returns>
        public int Count(string searchValue)
        {
            int rowCount = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (SqlConnection connection = new SqlConnection(connectionString)) //tao 1 doi tuong ket noi csdl
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())  //tao doi tuong command chua 1 cau lenh dung de thuc thi yeu cau truy van du lieu
                {
                    cmd.CommandText = "select COUNT(*) from Shippers where (@searchValue = N'') or (CompanyName like @searchValue)";
                    cmd.CommandType = CommandType.Text; //cho biet lenh thuc thi la lenh dang gi
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return rowCount;
        }

        /// <summary>
        /// Xóa một hoặc nhiều người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete(int[] shippersIds)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Shippers
                                            WHERE(ShipperID = @shipperId)
                                              AND(ShipperID NOT IN(SELECT ShipperID FROM Orders))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@shipperId", SqlDbType.Int);
                foreach (int shipperId in shippersIds)
                {
                    cmd.Parameters["@shipperId"].Value = shipperId;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy thông tin của môt người giao hàng theo ID
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public Shipper Get(int shipperID)
        {
            Shipper data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Shippers WHERE ShipperID = @shipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@shipperID", shipperID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Shipper()
                        {
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Hàm lấy danh sách những người giao hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Shipper> List(int page, int pageSize, string searchValue)
        {
            List<Shipper> data = new List<Shipper>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString)) //tao 1 doi tuong ket noi csdl
            {
                // mo ket noi
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())  //tao doi tuong command chua 1 cau lenh dung de thuc thi yeu cau truy van du lieu
                {
                    //chuoi chua cau lenh can thuc thi
                    cmd.CommandText = @"select *
                                        from
                                        (
	                                        select * ,
			                                        ROW_NUMBER() over(order by ShipperID) as RowNumber
	                                        from	Shippers
	                                        where	(@searchValue = N'') or (CompanyName like @searchValue)
                                        ) as t
                                        where t.RowNumber between (@page -1) * @pageSize + 1 and @page * @pageSize
                                        order by t.RowNumber";
                    cmd.CommandType = CommandType.Text; //cho biet lenh thuc thi la lenh dang gi
                    cmd.Connection = connection;

                    //them gia tri cho cac tham so dau vao co trong cau lenh sql
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new Shipper()
                            {
                                ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                Phone = Convert.ToString(dbReader["Phone"]),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Cập nhật thông tin của một người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Shipper data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Shippers
                                    SET
                                        CompanyName = @CompanyName,
                                        Phone = @Phone
                                    WHERE
                                        ShipperID = @shipperID;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                //TODO: Bổ sung tham số cho lệnh cập nhật
                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@shipperID", data.ShipperID);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}