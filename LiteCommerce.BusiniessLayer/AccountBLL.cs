using LiteCommerce.DataLayer;
using LiteCommerce.DataLayer.SqlServer;
using LiteCommerce.DomainModels;
using System;

namespace LiteCommerce.BusiniessLayer
{
    public static class AccountBLL
    {
        private static IEmployeeDAL EmployeeDB { get; set; }
        private static string connectionString;

        public static void Initialize(string connectionString)
        {
            EmployeeDB = new DataLayer.SqlServer.EmployeeDAL(connectionString);
            AccountBLL.connectionString = connectionString;
        }

        #region Employee

        /// <summary>
        /// Kiểm tra đăng nhập
        /// </summary>
        /// <param name="email">Email đăngn nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns></returns>
        public static bool Login(string email, string password)
        {
            return EmployeeDB.Login(email, password);
        }

        /// <summary>
        /// Cập nhật mật khẩu theo email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Update(string email, string password)
        {
            return EmployeeDB.Update(email, password);
        }

        /// <summary>
        /// Lấy thông tin của nhân viên theo email
        /// Lấy mật khẩu cũ
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Employee GetPass(string email)
        {
            return EmployeeDB.Get(email);
        }

        /// <summary>
        /// Phân quyền theo username, password và loại user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public static UserAccount Authorize(string username, string password, UserAccountTypes userType)
        {
            IUserAccountDAL UserAccountDB = null;
            switch (userType)
            {
                case UserAccountTypes.Employee:
                    UserAccountDB = new EmployeeUserAccountDAL(connectionString);
                    break;

                case UserAccountTypes.Customer:
                    UserAccountDB = new CustomerUserAccountDAL(connectionString);
                    break;

                default:
                    throw new Exception("Usertype is not valid");
            }
            return UserAccountDB.Authorize(username, password);
        }

        public static bool GetEmail(string email)
        {
            return EmployeeDB.GetEmail(email);
        }
        #endregion Employee
    }
}