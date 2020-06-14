using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayer.SqlServer
{
    public class CategoryDAL : ICategoryDAL
    {
        private string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public CategoryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Bổ sung thông tin một thể loại
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Category data)
        {
            int categoryId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Categories
                                            (
                                            CategoryName,
                                            Description
                                            )
                                            VALUES
                                            (
                                            @CategoryName,
                                            @Description
                                            );
                                            SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);

                categoryId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return categoryId;
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
                searchValue = " % " + searchValue + "%";

            using (SqlConnection connection = new SqlConnection(connectionString)) //tao 1 doi tuong ket noi csdl
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())  //tao doi tuong command chua 1 cau lenh dung de thuc thi yeu cau truy van du lieu
                {
                    cmd.CommandText = "select COUNT(*) from Categories where (@searchValue = N'') or (CategoryName like @searchValue)";
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
        /// Xóa một hoặc nhiều thể loại theo ID
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete(int[] categoryIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Categories
                                            WHERE(CategoryID = @categoryId)
                                              AND(CategoryID NOT IN(SELECT CategoryID FROM Products))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@categoryId", SqlDbType.Int);
                foreach (int categoryId in categoryIDs)
                {
                    cmd.Parameters["@categoryId"].Value = categoryId;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy thông tin thể loại theo ID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category Get(int categoryID)
        {
            Category data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Categories WHERE CategoryID = @categoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Category()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Danh sách thông tin thể loại có phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Category> List(int page, int pageSize, string searchValue)
        {
            List<Category> data = new List<Category>();
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
			                                        ROW_NUMBER() over(order by CategoryID) as RowNumber
	                                        from	Categories
	                                        where	(@searchValue = N'') or (CategoryName like @searchValue)
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
                            data.Add(new Category()
                            {
                                CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                                CategoryName = Convert.ToString(dbReader["CategoryName"]),
                                Description = Convert.ToString(dbReader["Description"]),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách các thể loại
        /// </summary>
        /// <returns></returns>
        public List<Category> List()
        {
            List<Category> data = new List<Category>();
            using (SqlConnection connection = new SqlConnection(connectionString)) //tao 1 doi tuong ket noi csdl
            {
                // mo ket noi
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())  //tao doi tuong command chua 1 cau lenh dung de thuc thi yeu cau truy van du lieu
                {
                    //chuoi chua cau lenh can thuc thi
                    cmd.CommandText = @"select DISTINCT CategoryName,CategoryID from Categories order by CategoryName";
                    cmd.CommandType = CommandType.Text; //cho biet lenh thuc thi la lenh dang gi
                    cmd.Connection = connection;

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new Category()
                            {
                                CategoryName = Convert.ToString(dbReader["CategoryName"]),
                                CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Cập nhật thông tin của một thể loại
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Category data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Categories
                                    SET
                                        CategoryName = @CategoryName,
                                        Description = @Description
                                    WHERE
                                        CategoryID = @CategoryID;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}