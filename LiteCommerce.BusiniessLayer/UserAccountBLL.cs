using LiteCommerce.DataLayer;
using LiteCommerce.DomainModels;
using System;

namespace LiteCommerce.BusiniessLayer
{
    public static class UserAccountBLL
    {
        public static IUserAccountDAL UserAccountDB { get; set; }
        private static string connectionString;

        public static void Initialize(string connectionString)
        {
            UserAccountDB = new DataLayer.SqlServer.EmployeeUserAccountDAL(connectionString);
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

        public static bool UserAccount_IsEmailExist(string emailID)
        {
            return UserAccountDB.IsEmailExist(emailID);
        }

        public static UserAccount UserAccount_GetUserByResetPasswordCode(string id)
        {
            return UserAccountDB.GetUserByResetPasswordCode(id);
        }

        public static bool UserAccount_AddResetCode(string email, string ResetPasswordCode)
        {
            return UserAccountDB.AddResetCode(email, ResetPasswordCode);
        }

        public static UserAccount UserAccount_GetUser(string email)
        {
            return UserAccountDB.User(email);
        }

        public static bool UserAccount_ChangePassword(string userID, string newPass)
        {
            return UserAccountDB.ChangePassword(userID, newPass);
        }
    }
}