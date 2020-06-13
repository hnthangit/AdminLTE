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
        public static bool Login(string email, string password)
        {
            return EmployeeDB.Login(email, password);
        }
        
        public static bool Update(string email, string password)
        {
            return EmployeeDB.Update(email, password);
        }
        public static Employee GetPass(string email)
        {
            return EmployeeDB.Get(email);
        }
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
        #endregion Employee
    }
}