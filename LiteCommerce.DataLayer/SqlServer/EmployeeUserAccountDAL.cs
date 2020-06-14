using LiteCommerce.DomainModels;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayer.SqlServer
{
    /// <summary>
    /// Kiểm tra thông tin đăng nhập của nhân viên
    /// </summary>
    public class EmployeeUserAccountDAL : IUserAccountDAL
    {
        private string connectionString;

        public EmployeeUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Authorize nhân viên
        /// </summary>
        /// <param name="userName">Địa chỉ email của nhân viên</param>
        /// <param name="password">Mật khẩu MD5</param>
        /// <returns></returns>
        public UserAccount Authorize(string userName, string password)
        {
            UserAccount userAccount = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE Email = @email and Password = @password";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@email", userName);
                cmd.Parameters.AddWithValue("@password", password);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        userAccount = new UserAccount()
                        {
                            UserID = Convert.ToString(dbReader["Email"]),
                            FullName = Convert.ToString(dbReader["LastName"] + " " + Convert.ToString(dbReader["FirstName"])),
                            Photo = Convert.ToString(dbReader["PhotoPath"]),
                            GroupName = Convert.ToString(dbReader["GroupName"])
                        };
                    }
                }

                connection.Close();
            }
            return userAccount;
        }
    }
}