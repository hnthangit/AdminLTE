using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayer.SqlServer
{
    /// <summary>
    /// Triển khai hàm cho interface Order
    /// </summary>
    public class OrderDAL : IOrderDAL
    {
        private string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Thêm mới một đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Mã đơn hàng vừa thêm</returns>
        public int Add(Order data)
        {
            int orderId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Suppliers
                                          (
                                                OrderID,
                                                CustomerID,
                                                EmployeeID,
                                                OrderDate,
                                                RequiredDate,
                                                ShippedDate,
                                                ShipperID,
                                                Freight,
                                                ShipAddress,
                                                ShipCity,
                                                ShipCountry
                                            )
                                            VALUES
                                            (
                                                @OrderID,
                                                @CustomerID,
                                                @EmployeeID,
                                                @OrderDate,
                                                @RequiredDate,
                                                @ShippedDate,
                                                @ShipperID,
                                                @Freight,
                                                @ShipAddress,
                                                @ShipCity,
                                                @ShipCountry
                                            );
                                            SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@OrderID", data.OrderID);
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@OrderDate", data.OrderDate);
                cmd.Parameters.AddWithValue("@RequiredDate", data.RequiredDate);
                cmd.Parameters.AddWithValue("@ShippedDate", data.ShippedDate);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@Freight", data.Freight);
                cmd.Parameters.AddWithValue("@ShipAddress", data.ShipAddress);
                cmd.Parameters.AddWithValue("@ShipCity", data.ShipCity);
                cmd.Parameters.AddWithValue("@ShipCountry", data.ShipCountry);

                orderId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return orderId;
        }

        /// <summary>
        /// Đếm số truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <param name="customerId">Mã khách hàng</param>
        /// <param name="employeeId">Mã nhân viên</param>
        /// <param name="shipperId">Mã người giao hàng</param>
        /// <returns>Số kết quả tìm được</returns>
        public int Count(string searchValue, int customerId, int employeeId, int shipperId)
        {
            int rowCount = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select COUNT(*) from Orders where ((@searchValue = N'') or(ShipCity like @searchValue)) and
                                                    ((@customerId = N'') or(CustomerID = @customerId)) and
                                                   ((@employeeId = N'') or(EmployeeID = @employeeId)) and
                                                    ((@shipperId = N'') or(ShipperID = @shipperId))";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    cmd.Parameters.AddWithValue("@shipperId", shipperId);

                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return rowCount;
        }

        /// <summary>
        /// Xóa một hay nhiều đơn hàng theo mã đơn hàng
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        public bool Delete(int[] orderIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Orders
                                            WHERE(OrderID = @OrderID)
                                            AND(OrderID NOT IN(SELECT OrderID FROM OrderDetails))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@OrderID", SqlDbType.Int);
                foreach (int orderID in orderIDs)
                {
                    cmd.Parameters["@OrderID"].Value = orderID;
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy thông tin của một đơn hàng theo mã đơn hàng
        /// </summary>
        /// <param name="orderID">Mã đơn hàng</param>
        /// <returns></returns>
        public Order Get(int orderID)
        {
            Order data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Orders WHERE OrderID = @OrderID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Order()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            CustomerID = Convert.ToString(dbReader["CustomerID"]),
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            OrderDate = Convert.ToDateTime(dbReader["OrderDate"]),
                            RequiredDate = Convert.ToDateTime(dbReader["RequiredDate"]),
                            ShippedDate = Convert.ToDateTime(dbReader["ShippedDate"]),
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            Freight = Convert.ToDecimal(dbReader["Freight"]),
                            ShipAddress = Convert.ToString(dbReader["ShipAddress"]),
                            ShipCity = Convert.ToString(dbReader["ShipCity"]),
                            ShipCountry = Convert.ToString(dbReader["ShipCountry"]),
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Danh sách các đơn hàng theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="customerId"></param>
        /// <param name="employeeId"></param>
        /// <param name="shipperId"></param>
        /// <returns></returns>
        public List<Order> List(int page, int pageSize, string searchValue, int customerId, int employeeId, int shipperId)
        {
            List<Order> data = new List<Order>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select *
                                        from
                                        (
	                                        select *,
	                                        ROW_NUMBER() over(order by CustomerID) as RowNumber
	                                        from Orders
	                                        where ((@searchValue = N'') or (ShipCity like @searchValue)) and
		                                        ((@customerId = N'') or(CustomerID = @customerId)) and
		                                        ((@employeeId = N'') or(EmployeeID = @employeeId)) and
		                                        ((@shipperId = N'') or(ShipperID = @shipperId))
                                        ) as t
                                        where t.RowNumber between (@page-1)*@pageSize + 1 and @page*@pageSize
                                        order by t.RowNumber";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    cmd.Parameters.AddWithValue("@shipperId", shipperId);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new Order()
                            {
                                OrderID = Convert.ToInt32(dbReader["OrderID"]),
                                CustomerID = Convert.ToString(dbReader["CustomerID"]),
                                EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                                OrderDate = Convert.ToDateTime(dbReader["OrderDate"]),
                                RequiredDate = Convert.ToDateTime(dbReader["RequiredDate"]),
                                ShippedDate = Convert.ToDateTime(dbReader["ShippedDate"]),
                                ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                                Freight = Convert.ToDecimal(dbReader["Freight"]),
                                ShipAddress = Convert.ToString(dbReader["ShipAddress"]),
                                ShipCity = Convert.ToString(dbReader["ShipCity"]),
                                ShipCountry = Convert.ToString(dbReader["ShipCountry"]),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Cập nhật một đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Order data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Products
                                    SET
                                        CustomerID = @CustomerID,
                                        EmployeeID = @EmployeeID,
                                        OrderDate = @OrderDate,
                                        RequiredDate = @RequiredDate,
                                        ShippedDate = @ShippedDate,
                                        ShipperID = @ShipperID,
                                        Freight = @Freight,
                                        ShipAddress = @ShipAddress,
                                        ShipCity = @ShipCity,
                                        ShipCountry = @ShipCountry
                                    WHERE
                                        OrderID = @OrderID;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@OrderDate", data.OrderDate);
                cmd.Parameters.AddWithValue("@RequiredDate", data.RequiredDate);
                cmd.Parameters.AddWithValue("@ShippedDate", data.ShippedDate);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@Freight", data.Freight);
                cmd.Parameters.AddWithValue("@ShipAddress", data.ShipAddress);
                cmd.Parameters.AddWithValue("@ShipCity", data.ShipCity);
                cmd.Parameters.AddWithValue("@ShipCountry", data.ShipCountry);
                cmd.Parameters.AddWithValue("@OrderID", data.OrderID);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}