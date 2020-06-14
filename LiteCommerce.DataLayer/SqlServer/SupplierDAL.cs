using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayer.SqlServer
{
    /// <summary>
    /// Triển khai các hàm cho interface Supllier
    /// </summary>
    public class SupplierDAL : ISupplierDAL
    {
        private string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Bố sung một nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Supplier data)
        {
            int supplierId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Suppliers
                                          (
	                                          CompanyName,
	                                          ContactName,
	                                          ContactTitle,
	                                          Address,
	                                          City,
	                                          Country,
	                                          Phone,
	                                          Fax,
	                                          HomePage
                                          )
                                          VALUES
                                          (
	                                          @CompanyName,
	                                          @ContactName,
	                                          @ContactTitle,
	                                          @Address,
	                                          @City,
	                                          @Country,
	                                          @Phone,
	                                          @Fax,
	                                          @HomePage
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@Fax", data.Fax);
                cmd.Parameters.AddWithValue("@HomePage", data.HomePage);

                supplierId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return supplierId;
        }

        /// <summary>
        /// Đếm số lượng truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
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
                    cmd.CommandText = "select COUNT(*) from Suppliers where (@searchValue = N'') or (CompanyName like @searchValue)";
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
        /// Xóa một hoặc nhiều nhà cung cấp theo ID
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        public bool Delete(int[] supplierIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Suppliers
                                            WHERE(SupplierID = @supplierId)
                                              AND(SupplierID NOT IN(SELECT SupplierID FROM Products))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@supplierId", SqlDbType.Int);
                foreach (int supplierId in supplierIDs)
                {
                    cmd.Parameters["@supplierId"].Value = supplierId;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy thông tin nhà cung cấp theo ID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public Supplier Get(int supplierID)
        {
            Supplier data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Suppliers WHERE SupplierID = @supplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@supplierID", supplierID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                            Fax = Convert.ToString(dbReader["Fax"]),
                            HomePage = Convert.ToString(dbReader["HomePage"]),
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Danh sach cac Supplier
        /// </summary>
        /// <param name="page">Trang hien tai</param>
        /// <param name="pageSize">Tong so trang</param>
        /// <param name="searchValue">Du lieu can tim kiem</param>
        /// <returns></returns>
        public List<Supplier> List(int page, int pageSize, string searchValue)
        {
            List<Supplier> data = new List<Supplier>();
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
			                                        ROW_NUMBER() over(order by SupplierID) as RowNumber
	                                        from	Suppliers
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

                    //thuc thi cau lenh: sau khi co doi tuong cmd thi tien hanh thuc thi lenh => ket qua tra ve dataReader chua du lieu truy van dc
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        //truyen du lieu
                        while (dbReader.Read())
                        {
                            data.Add(new Supplier()
                            {
                                SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                ContactName = Convert.ToString(dbReader["ContactName"]),
                                ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                                Address = Convert.ToString(dbReader["Address"]),
                                //TODO: lam not cac truong con lai
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                Phone = Convert.ToString(dbReader["Phone"]),
                                Fax = Convert.ToString(dbReader["Fax"]),
                                HomePage = Convert.ToString(dbReader["HomePage"]),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Lấy danh sách tất cả các nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public List<Supplier> List()
        {
            List<Supplier> data = new List<Supplier>();
            //TODO: Truy vấn dữ liệu từ CSDL ...
            using (SqlConnection connection = new SqlConnection(connectionString)) //tao 1 doi tuong ket noi csdl
            {
                // mo ket noi
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())  //tao doi tuong command chua 1 cau lenh dung de thuc thi yeu cau truy van du lieu
                {
                    //chuoi chua cau lenh can thuc thi
                    cmd.CommandText = @"select DISTINCT CompanyName, SupplierID from Suppliers order by CompanyName";
                    cmd.CommandType = CommandType.Text; //cho biet lenh thuc thi la lenh dang gi
                    cmd.Connection = connection;

                    //them gia tri cho cac tham so dau vao co trong cau lenh sql

                    //thuc thi cau lenh: sau khi co doi tuong cmd thi tien hanh thuc thi lenh => ket qua tra ve dataReader chua du lieu truy van dc
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        //truyen du lieu
                        while (dbReader.Read())
                        {
                            data.Add(new Supplier()
                            {
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Cập nhật thông tin của một nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Supplier data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Suppliers
                                    SET
                                        CompanyName = @CompanyName,
                                        ContactName = @ContactName,
                                        ContactTitle = @ContactTitle,
                                        Address = @Address,
                                        City = @City,
                                        Country = @Country,
                                        Phone = @Phone,
                                        Fax = @Fax,
                                        HomePage = @HomePage
                                    WHERE
                                        SupplierID = @SupplierID;";
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
                cmd.Parameters.AddWithValue("@HomePage", data.HomePage);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}