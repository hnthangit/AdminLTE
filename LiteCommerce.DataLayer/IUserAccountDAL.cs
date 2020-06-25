using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// Cung cấp một số chức năng của account
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Kiểm tra thông tin đăng nhập của user có hợp lệ hay không
        /// Nếu hợp lệ trả về thông tin của user
        /// Ngược lại hàm trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);

        /// <summary>
        ///
        /// </summary>
        /// <param name="emailID"></param>
        /// <returns></returns>
        bool IsEmailExist(string emailID);

        /// <summary>
        ///
        /// </summary>
        /// <param name="resetCode"></param>
        /// <returns></returns>
        UserAccount GetUserByResetPasswordCode(string resetCode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ResetPasswordCode"></param>
        /// <returns></returns>
        bool AddResetCode(string email, string ResetPasswordCode);

        UserAccount User(string userID);

        bool ChangePassword(string userID, string newPass);
    }
}