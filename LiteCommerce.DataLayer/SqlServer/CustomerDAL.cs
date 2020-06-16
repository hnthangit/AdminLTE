using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayer.SqlServer
{
    public class CustomerDAL : ICustomerDAL
    {
        private string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomerDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Thêm thông tin một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Customer data)
        {
            int customerId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Customers
                                            (
	                                            CustomerID,
	                                            CompanyName,
	                                            ContactName,
	                                            ContactTitle,
	                                            Address,
	                                            City,
	                                            Country,
	                                            Phone,
	                                            Fax
                                            )
                                            VALUES
                                            (
	                                            @CustomerID,
	                                            @CompanyName,
	                                            @ContactName,
	                                            @ContactTitle,
	                                            @Address,
	                                            @City,
	                                            @Country,
	                                            @Phone,
	                                            @Fax
                                            );
                                            SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@Fax", data.Fax);

                customerId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return customerId;
        }

        /// <summary>
        /// Xóa một hoặc nhiều khách hàng theo ID
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete(int[] customerIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Customers
                                            WHERE(CustomerID = @customerId)
                                              AND(CustomerID NOT IN(SELECT CustomerID FROM Orders))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@customerId", SqlDbType.Int);
                foreach (int customerId in customerIDs)
                {
                    cmd.Parameters["@customerId"].Value = customerId;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy thông tin của một khách hàng theo ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer Get(string customerID)
        {
            Customer data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Customers WHERE CustomerID = @customerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@customerID", customerID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Customer()
                        {
                            CustomerID = Convert.ToString(dbReader["CustomerID"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                            Fax = Convert.ToString(dbReader["Fax"]),
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Danh sách khách hàng theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Customer> List(int page, int pageSize, string searchValue)
        {
            List<Customer> data = new List<Customer>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            //TODO: Truy vấn dữ liệu từ CSDL ...
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
			                                        ROW_NUMBER() over(order by CustomerID) as RowNumber
	                                        from	Customers
	                                        where	(@searchValue = N'') or (ContactName like @searchValue)
                                        ) as t
                                        where t.RowNumber between (@page -1) * @pageSize + 1 and @page * @pageSize
                                        order by t.RowNumber";
                    cmd.CommandType = CommandType.Text; //cho biet lenh thuc thi la lenh dang gi
                    cmd.Connection = connection;

                    //them gia tri cho cac tham so dau vao co trong cau lenh sql
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    //thuc thi cau lenh: sau khi co doi tuong cmd thi tien hanh thuc thi lenh => ket qua tra ve dataReader chua du lieu truy van dc
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        //truyen du lieu
                        while (dbReader.Read())
                        {
                            data.Add(new Customer()
                            {
                                CustomerID = Convert.ToString(dbReader["CustomerID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                ContactName = Convert.ToString(dbReader["ContactName"]),
                                ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                                Address = Convert.ToString(dbReader["Address"]),
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                Phone = Convert.ToString(dbReader["Phone"]),
                                Fax = Convert.ToString(dbReader["Fax"]),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Lấy thông tin của một khách hàng.
        /// Gồm: mã khách hàng và tên công ty
        /// </summary>
        /// <returns></returns>
        public List<Customer> List()
        {
            List<Customer> data = new List<Customer>();
            //TODO: Truy vấn dữ liệu từ CSDL ...
            using (SqlConnection connection = new SqlConnection(connectionString)) //tao 1 doi tuong ket noi csdl
            {
                // mo ket noi
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())  //tao doi tuong command chua 1 cau lenh dung de thuc thi yeu cau truy van du lieu
                {
                    //chuoi chua cau lenh can thuc thi
                    cmd.CommandText = @"select DISTINCT CustomerID, CompanyName from Customers order by CompanyName";
                    cmd.CommandType = CommandType.Text; //cho biet lenh thuc thi la lenh dang gi
                    cmd.Connection = connection;

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        //truyen du lieu
                        while (dbReader.Read())
                        {
                            data.Add(new Customer()
                            {
                                CustomerID = Convert.ToString(dbReader["CustomerID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Cập nhật thông tin một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Customer data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Customers
                                    SET
                                        CompanyName = @CompanyName,
                                        ContactName = @ContactName,
                                        ContactTitle = @ContactTitle,
                                        Address = @Address,
                                        City = @City,
                                        Country = @Country,
                                        Phone = @Phone,
                                        Fax = @Fax
                                    WHERE
                                        CustomerID = @CustomerID;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                //TODO: Bổ sung tham số cho lệnh cập nhật
                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@Fax", data.Fax);
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }

        /// <summary>
        /// Đếm số lượng truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            //TODO: sửa hàm count lại
            int rowCount = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (SqlConnection connection = new SqlConnection(connectionString)) //tao 1 doi tuong ket noi csdl
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())  //tao doi tuong command chua 1 cau lenh dung de thuc thi yeu cau truy van du lieu
                {
                    cmd.CommandText = "select COUNT(*) from Customers where (@searchValue = N'') or (ContactName like @searchValue)";
                    cmd.CommandType = CommandType.Text; //cho biet lenh thuc thi la lenh dang gi
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return rowCount;
        }
    }
}