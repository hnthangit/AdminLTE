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
        /// Kiểm tra Email đã tồn tại hay chưa
        /// </summary>
        /// <param name="emailID"></param>
        /// <returns></returns>
        bool IsEmailExist(string emailID);

        /// <summary>
        /// Lấy mã reset mật khẩu
        /// </summary>
        /// <param name="resetCode"></param>
        /// <returns></returns>
        UserAccount GetUserByResetPasswordCode(string resetCode);

        /// <summary>
        /// Thêm mã đặt lại mật khẩu vào db
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ResetPasswordCode"></param>
        /// <returns></returns>
        bool AddResetCode(string email, string ResetPasswordCode);

        /// <summary>
        /// Lấy thông tin useraccount theo userID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        UserAccount User(string userID);

        /// <summary>
        /// Thay đổi mật khẩu ở trang forgotpass
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="newPass"></param>
        /// <returns></returns>
        bool ChangePassword(string userID, string newPass);
    }
}