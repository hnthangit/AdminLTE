using LiteCommerce.DomainModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

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

        public bool IsEmailExist(string emailID)
        {
            int isSuccess = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"Select Email from Employees
                                                Where Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@Email", emailID);
                    isSuccess = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
                connection.Close();
            }
            return isSuccess > 0;
        }

        public UserAccount GetUserByResetPasswordCode(string resetCode)
        {
            UserAccount data = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"Select Email,ResetPasswordCode from Employees
                                           Where ResetPasswordCode = @id";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@id", resetCode);

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            data = new UserAccount()
                            {
                                UserID = Convert.ToString(dataReader["Email"]),
                                ResetPasswordCode = Convert.ToString(dataReader["ResetPasswordCode"]),
                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool AddResetCode(string email, string ResetPasswordCode)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"UPDATE Employees
                                                SET
                                                ResetPasswordCode = @ResetPasswordCode
                                                WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ResetPasswordCode", ResetPasswordCode);
                    cmd.Parameters.AddWithValue("@Email", email);

                    rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
                connection.Close();
            }
            return rowsAffected > 0;
        }

        public UserAccount User(string email)
        {
            UserAccount user = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select Password, Email,ResetPasswordCode from Employees Where Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        if (dbReader.Read())
                        {
                            user = new UserAccount()
                            {
                                Password = Convert.ToString(dbReader["Password"]),
                                UserID = Convert.ToString(dbReader["Email"]),
                                ResetPasswordCode = Convert.ToString(dbReader["ResetPasswordCode"])

                            };
                        }
                }
                connection.Close();
            }
            return user;
        }

        private string GetMD5(string str)
        {
            try
            {
                MD5 mD5 = MD5CryptoServiceProvider.Create();
                byte[] dataMd5 = mD5.ComputeHash(Encoding.Default.GetBytes(str));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < dataMd5.Length; i++)
                    stringBuilder.AppendFormat("{0:x2}", dataMd5[i]);
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangePassword(string userID, string newPass)
        {
            int isSuccess = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"Update Employees 
                                                set 
                                                Password = @NewPassword
                                                Where Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@NewPassword", GetMD5(newPass));
                    cmd.Parameters.AddWithValue("@Email", userID);
                    isSuccess = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
                connection.Close();
            }
            return isSuccess > 0;
        }
    }
}