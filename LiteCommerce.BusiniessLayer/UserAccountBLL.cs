using LiteCommerce.DomainModels;
using LiteCommerce.DataLayer;
using System;

namespace LiteCommerce.BusiniessLayer
{
    public static class UserAccountBLL
    {
        private static string connectionString;

        public static void Initialize(string connectionString)
        {
            UserAccountBLL.connectionString = connectionString;
        }

        public static UserAccount Authorize(string userName, string password, UserAccountTypes userType)
        {
            IUserAccountDAL userAccountDB;
            switch (userType)
            {
                case UserAccountTypes.Employee: userAccountDB = new DataLayer.SqlServer.EmployeeUserAccountDAL(connectionString); break;
                case UserAccountTypes.Customer: userAccountDB = new DataLayer.SqlServer.CustomerUserAccountDAL(connectionString); break;
                default:
                    throw new Exception("UserTypes is not valid");
            }
            return userAccountDB.Authorize(userName, password);
        }
    }
}