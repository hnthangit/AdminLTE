using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayer.SqlServer
{
    public class CustomerUserAccountDAL : IUserAccountDAL
    {
        private string connectionString;
        public CustomerUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddResetCode(string email, string ResetPasswordCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Authorize nhân viên
        /// </summary>
        /// <param name="userName">Địa chỉ email của nhân viên</param>
        /// <param name="password">Mật khẩu MD5</param>
        /// <returns></returns>
        public UserAccount Authorize(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string userID, string newPass)
        {
            throw new NotImplementedException();
        }

        public UserAccount GetUserByResetPasswordCode(string resetCode)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailExist(string emailID)
        {
            throw new NotImplementedException();
        }

        public UserAccount User(string userID)
        {
            throw new NotImplementedException();
        }
    }
}
